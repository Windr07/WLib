using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using WLib.ArcGis.Carto.MapExport;
using WLib.ArcGis.Carto.MapExport.Base;
using WLib.ArcGis.Data;
using WLib.ArcGis.GeoDatabase.FeatClass;
using WLib.Model;

namespace WLib.Samples.WinForm.SubForm
{
    public partial class ExportMapForm : Form
    {
        private bool _stopRuning;
        private const string VillageMap = "村级样点分布图";
        private const string TownMap = "镇级样点分布图";
        private const string DistrictMap = "县级样点分布图";
        private readonly Dictionary<string, string> _templateDict = new Dictionary<string, string>
        {
            {VillageMap,"XX县XX村委会耕地分等样点分布图（村级）" },
            {TownMap,"XX县XX乡耕地分等样点分布图（镇级）" },
            {DistrictMap,"XX县耕地分等样点分布图（县级）" },
        };

        public ExportMapForm()
        {
            InitializeComponent();

            cmbExportMapType.Items.AddRange(_templateDict.Keys.ToArray());
            cmbExportMapType.SelectedIndex = 0;
            cmbPicExtension.SelectedIndex = 0;

            pathBoxDir.SelectPath(AppDomain.CurrentDomain.BaseDirectory + @"Data\输出");
            Directory.CreateDirectory(pathBoxDir.Path);
            pathBoxDb.SelectPath(@"F:\工作事项\11、耕地质量系列项目\各地项目\广西耕地质量\样点分布图\制图数据库.mdb");
        }

        private MapExportInfo ExportByVillage(string mdbPath, string regionName, double dpi, string extesion, ItemObject itemObject)
        {
            var xzqmc = itemObject.Name;
            var xzm = itemObject.Classify;
            var whereClause = $@"[ZLDWMC] = '{xzqmc}'";
            var cfg = new MapExportInfo
            {
                CfgName = @"测试配置",
                TemplateMxdPath = pathBoxTemplate.Path,
                ExportDirectory = pathBoxDir.Path,
                ExportFileName = $"{regionName}{xzm}耕地分等样点分布图"
            };
            cfg.ExportPictures.Add(dpi, true, extesion);
            cfg.Elements.Add("分布图", EPageElementType.Text, $"{regionName}{xzm}耕地分等样点分布图");
            cfg.MapFrames.Add("图层", 0,
                new LayerInfo("调查样点", mdbPath, "调查样点80坐标", 0, whereClause),
                new LayerInfo("XZQJX", mdbPath, "XZQJX", 1),
                new LayerInfo("DLTB提取", mdbPath, "DLTB提取", 2, whereClause),
                new LayerInfo("分等单元", mdbPath, "分等单元", 3, whereClause),
                new LayerInfo("XZQ", mdbPath, "XZQ", 4, $@"[XZM] = '{xzm}'") { ZoomTo = true },
                new LayerInfo("XZQ", mdbPath, "XZQ", 5)
            );
            return cfg;
        }

        private MapExportInfo ExportByTown(string mdbPath, string regionName, double dpi, string extesion, ItemObject itemObject)
        {
            var zmc = itemObject.Classify;
            var whereClause = $@"[ZMC] = '{zmc}'";
            var cfg = new MapExportInfo
            {
                CfgName = @"测试配置",
                TemplateMxdPath = pathBoxTemplate.Path,
                ExportDirectory = pathBoxDir.Path,
                ExportFileName = $"{regionName}{zmc}耕地分等样点分布图"
            };
            cfg.ExportPictures.Add(dpi, true, extesion);
            cfg.Elements.Add("分布图", EPageElementType.Text, $"{regionName}{zmc}耕地分等样点分布图");
            cfg.MapFrames.Add("图层", 0,
                new LayerInfo("调查样点", mdbPath, "调查样点80坐标", 0, whereClause),
                new LayerInfo("XZQJX", mdbPath, "XZQJX", 1),
                new LayerInfo("DLTB提取", mdbPath, "DLTB提取", 2, whereClause),
                new LayerInfo("XZQ", mdbPath, "XZQ", 3, whereClause, true),
                new LayerInfo("XZQ_按镇", mdbPath, "XZQ_按镇", 4),
                new LayerInfo("分等单元", mdbPath, "分等单元", 5, $@"ZMC_1 = '{zmc}'")
            );
            return cfg;
        }

        private MapExportInfo ExportByDistrict(string mdbPath, string regionName, double dpi, string extesion)
        {
            var cfg = new MapExportInfo
            {
                CfgName = @"测试配置",
                TemplateMxdPath = pathBoxTemplate.Path,
                ExportDirectory = pathBoxDir.Path,
                ExportFileName = $"{regionName}耕地分等样点分布图"
            };
            cfg.ExportPictures.Add(dpi, true, extesion);
            cfg.Elements.Add("分布图", EPageElementType.Text, $"{regionName}耕地分等样点分布图");
            cfg.MapFrames.Add("图层", 0,
                new LayerInfo("调查样点", mdbPath, "调查样点80坐标", 0),
                new LayerInfo("XZQJX", mdbPath, "XZQJX", 1),
                new LayerInfo("DLTB提取", mdbPath, "DLTB提取", 2),
                new LayerInfo("分等单元", mdbPath, "分等单元", 3),
                new LayerInfo("XZQ", mdbPath, "XZQ", 4, null, true),
                new LayerInfo("XZQ_按镇", mdbPath, "XZQ_按镇", 5)
            );
            return cfg;
        }

        private void ShowRegionList()
        {
            var path = pathBoxDb.Path.Trim();
            if (!File.Exists(path))
                return;
            var selectedItem = cmbExportMapType.SelectedItem.ToString();
            var itemObjects = new List<ItemObject>();
            if (selectedItem == VillageMap)
            {
                listBoxPlus1.Description = "行政村列表";
                var xzqInfos = FeatClassFromPath.FromPath(path + @"\XZQ").ConvertToObject<VillageInfo>();
                for (int i = 0; i < xzqInfos.Count; i++)
                    itemObjects.Add(new ItemObject(xzqInfos[i].XZQDM, xzqInfos[i].XZQMC, i + 1, xzqInfos[i].XZM));
            }
            else if (selectedItem == TownMap)
            {
                listBoxPlus1.Description = "镇列表";
                var xzqInfos = FeatClassFromPath.FromPath(path + @"\XZQ_按镇").ConvertToObject<TownInfo>();
                for (int i = 0; i < xzqInfos.Count; i++)
                    itemObjects.Add(new ItemObject(xzqInfos[i].ZDM, xzqInfos[i].ZMC, i + 1, xzqInfos[i].ZMC));
            }
            else if (selectedItem == DistrictMap)
            {
                listBoxPlus1.Description = "县列表";
                itemObjects.Add(new ItemObject("", txtRegionName.Text));
            }
            listBoxPlus1.Init(itemObjects.ToArray());
        }

        private void ChangeViews(bool isStarted)
        {
            _stopRuning = false;
            listBoxPlus1.Enabled = !isStarted;
            pathBoxDir.OptEnable = !isStarted;
            pathBoxTemplate.OptEnable = !isStarted;
            pathBoxDb.OptEnable = !isStarted;
            btnStop.Enabled = isStarted;
            btnExpt.Enabled = !isStarted;
            panel1.Enabled = !isStarted;
            cmbExportMapType.Enabled = !isStarted;
            Application.DoEvents();
        }

        private void btnExprt_Click(object sender, EventArgs e)
        {
            try
            {
                ChangeViews(true);
                var regionName = this.txtRegionName.Text.Trim();
                var mdbPath = pathBoxDb.Path;
                var itemObjects = listBoxPlus1.GetCheckedItems().Cast<ItemObject>().ToArray();
                var selectItem = this.cmbExportMapType.SelectedItem.ToString();
                var dpi = (double)this.numDpi.Value;
                var extesion = this.cmbPicExtension.SelectedItem.ToString();
                var mapExoprtHelper = new MapExportHelper();

                foreach (var itemObject in itemObjects)
                {
                    MapExportInfo cfg = null;
                    if (selectItem == VillageMap) cfg = ExportByVillage(mdbPath, regionName, dpi, extesion, itemObject);
                    else if (selectItem == TownMap) cfg = ExportByTown(mdbPath, regionName, dpi, extesion, itemObject);
                    else if (selectItem == DistrictMap) cfg = ExportByDistrict(mdbPath, regionName, dpi, extesion);

                    txtMessage.Text += $@"正在导出：{cfg.ExportFileName}" + Environment.NewLine;
                    Application.DoEvents();
                    mapExoprtHelper.ExportMap(cfg);
                    txtMessage.Text += $@"导出完成：{cfg.ExportFileName}" + Environment.NewLine;
                    Application.DoEvents();
                    if (_stopRuning)
                        break;
                }
                txtMessage.Text += @"导出完成！" + Environment.NewLine + Environment.NewLine;
            }
            catch (Exception ex) { txtMessage.Text += ex + Environment.NewLine + Environment.NewLine; }
            finally { ChangeViews(false); }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _stopRuning = true;
        }    

        private void pathBoxDb_AfeterSelectPath(object sender, EventArgs e)
        {
            try
            {
                ShowRegionList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cmbExportMapType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var selectedItem = cmbExportMapType.SelectedItem.ToString();
                pathBoxTemplate.SelectPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", _templateDict[selectedItem] + ".mxd"));
                ShowRegionList();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}
