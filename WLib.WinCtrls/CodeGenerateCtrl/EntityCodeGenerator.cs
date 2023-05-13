using Newtonsoft.Json;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WLib.Database;
using WLib.Database.DbBase;
using WLib.Database.ORM;
using WLib.WinCtrls.InputCtrl;

namespace WLib.WinCtrls.CodeGenerateCtrl
{
    /// <summary>
    /// 连接数据库，生成C#或Java实体类代码的控件
    /// </summary>
    public partial class EntityCodeGenerator : UserControl
    {
        private string _configPath = AppDomain.CurrentDomain.BaseDirectory + "entityCodeGenerator.json";

        public EntityCodeGenerator()
        {
            InitializeComponent();

            //绑定菜单事件
            Action<ListBox> addAction = listbox =>
            {
                var inputForm = new InputForm();
                if (inputForm.ShowDialog() == DialogResult.OK)
                    listbox.Items.Add(inputForm.KeyWord);
            };
            Action<ListBox> deleteAction = listbox =>
            {
                if (listbox.Items.Count > 0 && listbox.SelectedIndex >= 0)
                    listbox.Items.Remove(listbox.SelectedItem);
            };
            this.menuItemNamespaceAdd.Click += (sender, e) => addAction(this.listBoxCNamespaces);
            this.menuItemNamespaceDelete.Click += (sender, e) => deleteAction(this.listBoxCNamespaces);
            this.menuItemClassAttrAdd.Click += (sender, e) => addAction(this.listBoxCClassAttr);
            this.menuItemClassAttrDelete.Click += (sender, e) => deleteAction(this.listBoxCClassAttr);
            this.menuItemFieldAttrAdd.Click += (sender, e) => addAction(this.listBoxCFieldAttr);
            this.menuItemFieldAttrDelete.Click += (sender, e) => deleteAction(this.listBoxCFieldAttr);

            this.menuItemImportAdd.Click += (sender, e) => addAction(this.listBoxImports);
            this.menuItemImportDelete.Click += (sender, e) => deleteAction(this.listBoxImports);
            this.menuItemAtClassAdd.Click += (sender, e) => addAction(this.listBoxAtClass);
            this.menuItemAtClassDelete.Click += (sender, e) => deleteAction(this.listBoxAtClass);
            this.menuItemAtFieldAdd.Click += (sender, e) => addAction(this.listBoxAtField);
            this.menuItemAtFieldDelete.Click += (sender, e) => deleteAction(this.listBoxAtField);
        }

        private void EntityCodeGenerator_Load(object sender, EventArgs e)
        {
            this.cmbDbTypes.Items.AddRange(Enum.GetNames(typeof(EDbProviderType)));
            this.cmbDbTypes.SelectedIndex = 0;
            this.txtCSavePath.Text = AppDomain.CurrentDomain.BaseDirectory + "CSharpCode";
            this.txtJSavePath.Text = AppDomain.CurrentDomain.BaseDirectory + "JavaCode";

            //读取配置文件
            if (File.Exists(_configPath))
            {
                this.listBoxCNamespaces.Items.Clear();
                this.listBoxCClassAttr.Items.Clear();
                this.listBoxCFieldAttr.Items.Clear();
                this.listBoxImports.Items.Clear();
                this.listBoxAtClass.Items.Clear();
                this.listBoxAtField.Items.Clear();

                var json = File.ReadAllText(_configPath);
                var settings = JsonConvert.DeserializeObject<GeneratorSettings>(json);
                this.cmbDbTypes.SelectedIndex = (int)settings.DbType;
                this.txtConnectionStr.Text = settings.ConnectionString;

                this.txtCSavePath.Text = settings.CSharpSavePath;
                this.textBoxCNamespace.Text = settings.CSharpSettings.NameSpace;
                this.textBoxCExtends.Text = settings.CSharpSettings.Inherits;
                settings.CSharpSettings.Usings.ForEach(v => this.listBoxCNamespaces.Items.Add(v));
                settings.CSharpSettings.ClassAttributes.ForEach(v => this.listBoxCClassAttr.Items.Add(v));
                settings.CSharpSettings.PropertyAttributes.ForEach(v => this.listBoxCFieldAttr.Items.Add(v));

                this.txtJSavePath.Text = settings.JavaSavePath;
                this.txtPackage.Text = settings.JavaSettings.Package;
                this.txtExtendClass.Text = settings.JavaSettings.Extends;
                this.txtImplement.Text = settings.JavaSettings.Implements;
                settings.JavaSettings.Imports.ForEach(v => this.listBoxImports.Items.Add(v));
                settings.JavaSettings.ClassAnnotations.ForEach(v => this.listBoxAtClass.Items.Add(v));
                settings.JavaSettings.PropertyAnnotations.ForEach(v => this.listBoxAtField.Items.Add(v));
            }
            //else
            //{
            //    this.listBoxCNamespaces.Items.Add("System");
            //    this.listBoxCClassAttr.Items.Add("Serializable");
            //}
        }

        private void cmbDbTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.lblTips.Text = DbHelper.GetConnectionStringExample((EDbProviderType)Enum.Parse(typeof(EDbProviderType), this.cmbDbTypes.SelectedItem.ToString()));
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                this.cListBoxTables.Items.Clear();
                var dbType = (EDbProviderType)Enum.Parse(typeof(EDbProviderType), this.cmbDbTypes.SelectedItem.ToString());
                var creator = new DbToCSharpEntityCode(this.txtConnectionStr.Text.Trim(), dbType);
                creator.ErrorOccurred += (sender2, e2) => this.txtMsg.Text += e2.Data.Message + Environment.NewLine;
                var tableNames = creator.GetTableNames().ToList();
                tableNames.Sort();
                tableNames.ForEach(t => this.cListBoxTables.Items.Add(t, this.cbAll.Checked));
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString(), "", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void cbAll_CheckedChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < this.cListBoxTables.Items.Count; i++)
                this.cListBoxTables.SetItemChecked(i, this.cbAll.Checked);
        }

        private void btnSaveSettings_Click(object sender, EventArgs e)//保存到配置文件
        {
            var settings = new GeneratorSettings();
            settings.DbType = (EDbProviderType)this.cmbDbTypes.SelectedIndex;
            settings.ConnectionString = this.txtConnectionStr.Text;
            settings.CSharpSavePath = this.txtCSavePath.Text;

            settings.CSharpSettings.NameSpace = this.textBoxCNamespace.Text;
            settings.CSharpSettings.Inherits = this.textBoxCExtends.Text;
            settings.CSharpSettings.Usings = this.listBoxCNamespaces.Items.Cast<string>().ToList();
            settings.CSharpSettings.ClassAttributes = this.listBoxCClassAttr.Items.Cast<string>().ToList();
            settings.CSharpSettings.PropertyAttributes = this.listBoxCFieldAttr.Items.Cast<string>().ToList();

            settings.JavaSavePath = this.txtJSavePath.Text;
            settings.JavaSettings.Package = this.txtPackage.Text;
            settings.JavaSettings.Extends = this.txtExtendClass.Text;
            settings.JavaSettings.Implements = this.txtImplement.Text;
            settings.JavaSettings.Imports = this.listBoxImports.Items.Cast<string>().ToList();
            settings.JavaSettings.ClassAnnotations = this.listBoxAtClass.Items.Cast<string>().ToList();
            settings.JavaSettings.PropertyAnnotations = this.listBoxAtField.Items.Cast<string>().ToList();

            var json = JsonConvert.SerializeObject(settings);
            File.WriteAllText(_configPath, json);
        }


        #region C#界面
        private void btnCSelectDir_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog
            {
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory + "Code",
                ShowNewFolderButton = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                this.txtCSavePath.Text = dlg.SelectedPath;
        }

        private void btnCOpen_Click(object sender, EventArgs e) => Process.Start("explorer.exe", this.txtCSavePath.Text);

        private void btnCreateCSharpCode_Click(object sender, EventArgs e)
        {
            var dbType = (EDbProviderType)Enum.Parse(typeof(EDbProviderType), this.cmbDbTypes.SelectedItem.ToString());
            Task.Run(() =>
            {
                Invoke(new Action(() => this.progressBar1.Visible = true));

                Directory.CreateDirectory(this.txtCSavePath.Text);
                var tableNames = this.cListBoxTables.CheckedItems.Cast<string>().ToArray();
                var creator = new DbToCSharpEntityCode(this.txtConnectionStr.Text.Trim(), dbType);
                creator.ErrorOccurred += (sender2, e2) => { Invoke(new Action(() => this.txtMsg.Text += e2.Data.Message + Environment.NewLine)); };
                creator.NameSpace = this.textBoxCNamespace.Text.Trim();
                creator.Inherits = this.textBoxCExtends.Text.Trim();
                creator.Usings = this.listBoxCNamespaces.Items.Cast<string>().ToList();
                creator.ClassAttributes = this.listBoxCClassAttr.Items.Cast<string>().ToList();
                creator.PropertyAttributes = this.listBoxCFieldAttr.Items.Cast<string>().ToList();
                creator.ToEntityCode(this.txtCSavePath.Text, Encoding.UTF8, tableNames);

                Invoke(new Action(() =>
                {
                    this.progressBar1.Visible = false;
                    if (MessageBox.Show("生成完成！是否打开生成目录？", "生成完成", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        btnCOpen_Click(null, null);
                }));
            });
        }
        #endregion


        #region Java界面
        private void btnJSelect_Click(object sender, EventArgs e)
        {
            var dlg = new FolderBrowserDialog
            {
                SelectedPath = AppDomain.CurrentDomain.BaseDirectory + "Code",
                ShowNewFolderButton = true
            };
            if (dlg.ShowDialog() == DialogResult.OK)
                this.txtJSavePath.Text = dlg.SelectedPath;
        }

        private void btnJOpen_Click(object sender, EventArgs e) => Process.Start("explorer.exe", this.txtJSavePath.Text);


        private void btnCreateJavaCode_Click(object sender, EventArgs e)
        {
            var dbType = (EDbProviderType)Enum.Parse(typeof(EDbProviderType), this.cmbDbTypes.SelectedItem.ToString());
            Task.Run(() =>
            {
                Invoke(new Action(() => this.progressBar1.Visible = true));

                Directory.CreateDirectory(this.txtJSavePath.Text);
                var tableNames = this.cListBoxTables.CheckedItems.Cast<string>().ToArray();
                var creator = new DbToJavaEntityCode(this.txtConnectionStr.Text.Trim(), dbType);
                creator.ErrorOccurred += (sender2, e2) => { Invoke(new Action(() => this.txtJMsg.Text += e2.Data.Message + Environment.NewLine)); };
                creator.Package = this.txtPackage.Text.Trim();
                creator.Extends = this.txtExtendClass.Text.Trim();
                creator.Implements = this.txtImplement.Text.Trim();
                creator.Imports = this.listBoxImports.Items.Cast<string>().ToList();
                creator.ClassAnnotations = this.listBoxAtClass.Items.Cast<string>().ToList();
                creator.PropertyAnnotations = this.listBoxAtField.Items.Cast<string>().ToList();
                creator.ToEntityCode(this.txtJSavePath.Text, Encoding.UTF8, tableNames);

                Invoke(new Action(() =>
                {
                    this.progressBar1.Visible = false;
                    if (MessageBox.Show("生成完成！是否打开生成目录？", "生成完成", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                        btnJOpen_Click(null, null);
                }));
            });
        }
        #endregion
    }
}
