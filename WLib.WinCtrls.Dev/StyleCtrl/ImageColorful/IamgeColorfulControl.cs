using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using WLib.Attributes.Description;
using WLib.Drawing;

namespace WLib.WinCtrls.Dev.StyleCtrl.ImageColorful
{
    /// <summary>
    /// 图标色彩风格设置控件
    /// </summary>
    public partial class IamgeColorfulControl : DevExpress.XtraEditors.XtraUserControl
    {
        /// <summary>
        /// 图标源目录
        /// </summary>
        private string _imageDir;
        /// <summary>
        /// 图标转换成选中风格后的保存目录
        /// </summary>
        private string _selectedColorImageDir;
        /// <summary>
        /// 图标源目录
        /// </summary>
        public string ImageDir
        {
            get => _imageDir;
            set
            {
                _imageDir = value;
                if (_imageDir == null) throw new Exception($"图标源目录（{nameof(ImageDir)}）不能为空");
                if (!Path.IsPathRooted(_imageDir)) _imageDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, _imageDir);
                if (!Directory.Exists(_imageDir)) throw new Exception($"图标源目录（{nameof(ImageDir)}）“{_imageDir}”不存在");
            }
        }
        /// <summary>
        /// 图标转换成选中风格后的保存目录
        /// </summary>
        public string SelectedColorImageDir { get => _selectedColorImageDir ?? (_selectedColorImageDir = ImageDir); set => _selectedColorImageDir = value; }
        /// <summary>
        /// 选中的图标色彩风格类型
        /// </summary>
        public EImageColorType ImageColorType
        {
            get => this.listBoxColors.SelectedItem == null ? EImageColorType.Default : this.listBoxColors.SelectedItem.ToString().GetEnum<EImageColorType>();
            set => this.listBoxColors.SelectedItem = value.GetDescriptionEx();
        }
        /// <summary>
        /// 支持的图片格式
        /// </summary>
        public string[] ImageExtensions { get; set; } = new[] { ".png", ".jpg", ".jpeg", ".tif", "bmp", "ico" };
        /// <summary>
        /// 图标色彩风格改变事件
        /// </summary>
        public event EventHandler<ImageStyleChangedEventArgs> ImageColorStyleChanged;
        /// <summary>
        /// 单个图标色彩风格改变事件
        /// </summary>
        public event EventHandler<ImageStyleItemChangedEventArgs> ImageColorStyleItemChanged;


        /// <summary>
        /// 图标色彩风格设置控件
        /// </summary>
        public IamgeColorfulControl()
        {
            InitializeComponent();
            var descriptions = EnumDescriptionExHelper.GetDescriptionExs<EImageColorType>();
            this.listBoxColors.Items.AddRange(descriptions);
            this.listBoxColors.SelectedItem = EImageColorType.Default.GetDescriptionEx();
            this.cmbIconLib.Properties.Items.AddRange(new[] { "动物世界", "中国省份" });
        }
        /// <summary>
        /// 图标色彩风格设置控件
        /// </summary>
        /// <param name="iconDir">图标源目录</param>
        /// <param name="imageColorType">选中的图标色彩风格类型</param>
        public IamgeColorfulControl(string iconDir, EImageColorType imageColorType) : this()
        {
            this.ImageDir = iconDir;
            this.ImageColorType = imageColorType;
        }


        /// <summary>
        ///  获取 “key为图标源路径, value为转换风格后的图标保存路径” 的键值对
        /// </summary>
        /// <param name="sourceImageDir">图标源目录</param>
        /// <param name="targetImageDir">图标转换后的保存目录</param>
        private Dictionary<string, string> GetSourceTargetImagePathDict(string sourceImageDir, string targetImageDir)
        {
            var dict = new Dictionary<string, string>();
            Directory.CreateDirectory(targetImageDir);
            var paths = Directory.GetFiles(sourceImageDir).Where(v => ImageExtensions.Contains(Path.GetExtension(v))).ToArray();
            foreach (var path in paths)
            {
                var savePath = Path.Combine(targetImageDir, Path.GetFileName(path));
                if (!File.Exists(savePath))
                    dict.Add(path, savePath);
            }
            return dict;
        }
        /// <summary>
        /// 遍历源目录下的全部图标文件，修改其图标风格，保存到新目录下
        /// </summary>
        /// <param name="colors"></param>
        private void CreateColorfulImages(Color[] colors = null)
        {
            var pathDict = GetSourceTargetImagePathDict(ImageDir, SelectedColorImageDir);
            foreach (var pair in pathDict)
            {
                var image = Image.FromFile(pair.Key);
                var newBitmap = new Bitmap(image).ToColorType(ImageColorType, false, colors);//转换图标
                image.Dispose();//释放图标文件
                newBitmap.Save(pair.Value);
                ImageColorStyleItemChanged?.Invoke(this, new ImageStyleItemChangedEventArgs(pair.Key, pair.Value, ImageColorType));
            }
            ImageColorStyleChanged?.Invoke(this, new ImageStyleChangedEventArgs(ImageDir, SelectedColorImageDir, ImageColorType));
        }
        /// <summary>
        /// 将选定的图标库图标，依次对应并且替换源图标目录中的图标，保存到新图标目录中
        /// </summary>
        /// <param name="startIndex"></param>
        /// <param name="endIndex"></param>
        private void CreateMatchingImages(int startIndex, int endIndex, params Color[] colors)
        {
            var pathDict = GetSourceTargetImagePathDict(ImageDir, SelectedColorImageDir);
            int i = startIndex;
            foreach (var pair in pathDict)
            {
                if (i > endIndex) i = startIndex;
                var image = this.imageCollection1.Images[i++];
                var newBitmap = new Bitmap(image).ToColorType(ImageColorType, false, colors);
                newBitmap.Save(pair.Value);
                ImageColorStyleItemChanged?.Invoke(this, new ImageStyleItemChangedEventArgs(pair.Key, pair.Value, ImageColorType));
            }
            ImageColorStyleChanged?.Invoke(this, new ImageStyleChangedEventArgs(ImageDir, SelectedColorImageDir, ImageColorType));
        }


        private void listBoxColors_SelectedIndexChanged(object sender, EventArgs e)//选中图标风格类型后，创建该类型的图标
        {
            //获取选中的图标色彩风格
            var description = this.listBoxColors.SelectedItem.ToString();
            ImageColorType = EnumDescriptionExHelper.GetEnum<EImageColorType>(description);
            this.panelCustom.Enabled = ImageColorType == EImageColorType.Custom;
            if (ImageColorType == EImageColorType.Default)
            {
                SelectedColorImageDir = ImageDir;
                ImageColorStyleChanged?.Invoke(this, new ImageStyleChangedEventArgs(ImageDir, SelectedColorImageDir, ImageColorType));
                return;
            }

            //新风格图标保存目录
            SelectedColorImageDir = Path.Combine(ImageDir, description);

            //创建新风格图标
            CreateColorfulImages(new[] { this.colorStart.Color, this.colortEnd.Color });
        }

        private void btnReset_Click(object sender, EventArgs e)//重置图标
        {
            var paths = Directory.GetFiles(ImageDir).Where(v => ImageExtensions.Contains(Path.GetExtension(v))).ToArray();
            foreach (var description in EnumDescriptionExHelper.GetDescriptionExs<EImageColorType>())
            {
                var subDir = Path.Combine(ImageDir, description);
                if (!Directory.Exists(subDir)) continue;
                foreach (var path in paths)
                {
                    try
                    {
                        File.Delete(Path.Combine(subDir, Path.GetFileName(path)));
                    }
                    catch { }
                }
            }
        }

        private void btnOpenImageDir_Click(object sender, EventArgs e)//打开图标目录
        {
            System.Diagnostics.Process.Start(ImageDir);
        }

        private void colorPickEdit_EditValueChanged(object sender, EventArgs e)//选取色彩后，创建图标
        {
            btnReset_Click(null, null);
            CreateColorfulImages(new[] { this.colorStart.Color, this.colortEnd.Color });
        }

        private void cmbIconLib_SelectedIndexChanged(object sender, EventArgs e)//选取图标库列表项后，创建图标
        {
            btnReset_Click(null, null);
            switch (this.cmbIconLib.SelectedItem.ToString())
            {
                case "动物世界": CreateMatchingImages(0, 19, this.colorStart.Color, this.colortEnd.Color); break;
                case "中国省份": CreateMatchingImages(20, 49, this.colorStart.Color, this.colortEnd.Color); break;
            }
        }
    }
}
