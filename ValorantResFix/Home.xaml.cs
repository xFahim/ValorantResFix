using IniParser;
using IniParser.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ValorantResFix
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {
        List<string> _fnames = new List<string>();
        List<ScalabilityGroups> _SG = new List<ScalabilityGroups>();
        ScalabilityGroups backupData = new ScalabilityGroups();


        // ini parse
        FileIniDataParser parser = new FileIniDataParser();
        List<IniData> _iniList = new List<IniData>();


        public Home(List<string> fl)
        {
            InitializeComponent();

            _fnames = fl;

            StartUP();
        }

        private void StartUP()
        {
            ScalabilityGroups sg = new ScalabilityGroups();


            TotalFileTxt.Text = _fnames.Count.ToString();

            // load Data
            foreach(var s in _fnames)
            {
                IniData data = parser.ReadFile(s);
               
                try
                {
                    sg.ResolutionQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.ResolutionQuality"]);
                    sg.ViewDistanceQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.ViewDistanceQuality"]);
                    sg.AntiAliasingQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.AntiAliasingQuality"]);
                    sg.ShadowQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.ShadowQuality"]);
                    sg.PostProcessQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.PostProcessQuality"]);
                    sg.TextureQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.TextureQuality"]);
                    sg.EffectsQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.EffectsQuality"]);
                    sg.FoliageQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.FoliageQuality"]);
                    sg.ShadingQuality = (int)decimal.Parse(data["ScalabilityGroups"]["sg.ShadingQuality"]);
                    sg.FilePath = s;


                    _SG.Add(sg);
                    _iniList.Add(data);
                }
                catch
                {
                    continue;
                }
            }

            // View Data 
            if (_SG.Count() < 1)
            {
                MessageBox.Show("Something went wrong parsing the config file.");
                System.Windows.Application.Current.Shutdown();
            }
            else
            {
                // set the first elements data to the view 
                ResqualityBox.Text = _SG[0].ResolutionQuality.ToString();
                ViewDistanceQBox.Text = _SG[0].ViewDistanceQuality.ToString();
                AntiAliasisBox.Text = _SG[0].AntiAliasingQuality.ToString();
                ShadowQBox.Text = _SG[0].ShadowQuality.ToString();
                PostProcessBox.Text = _SG[0].PostProcessQuality.ToString();
                TextureQBox.Text = _SG[0].TextureQuality.ToString();
                EffectQBox.Text = _SG[0].EffectsQuality.ToString();
                FoliageQBox.Text = _SG[0].FoliageQuality.ToString();
                ShadingQBox.Text = _SG[0].ShadowQuality.ToString();
            }


            // check backup 
            if (File.Exists("Backup.dat"))
            {
                backupData = (ScalabilityGroups)Shelper.DeserializeObj("Backup.dat");

                if (backupData == null)
                {
                    MessageBox.Show("Failed parsing Backup File.\nBAckup-File might be corrupted. Please restart the application.");
                    Application.Current.Shutdown();
                }

            }
            else // writes backup
            {
                if (Shelper.SerializeObj(_SG[0], "Backup.dat") == 1)
                {
                    backupData = _SG[0];
                }
                else
                {
                    MessageBox.Show("Something went wrong while keeping the backup.\nPlease restart the application.");
                    Application.Current.Shutdown();
                }
            }

            LoadGrid.Visibility = Visibility.Collapsed;

        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            ScalabilityGroups sg = new ScalabilityGroups();
            // validations 
            // 1st validation -try convert str to int
            try
            {
                sg.ResolutionQuality = Int32.Parse(ResqualityBox.Text);
                sg.ViewDistanceQuality = Int32.Parse(ViewDistanceQBox.Text);
                sg.AntiAliasingQuality = Int32.Parse(AntiAliasisBox.Text);
                sg.ShadowQuality = Int32.Parse(ShadowQBox.Text);
                sg.PostProcessQuality = Int32.Parse(PostProcessBox.Text);
                sg.TextureQuality = Int32.Parse(TextureQBox.Text);
                sg.EffectsQuality = Int32.Parse(EffectQBox.Text);
                sg.FoliageQuality = Int32.Parse(FoliageQBox.Text);
                sg.ShadingQuality = Int32.Parse(ShadingQBox.Text);

            }
            catch
            {
                MessageBox.Show("Invalid Data Entered. \nMake sure all the field has valid integer value and try again.","Error");
                return;
            }
            
            // second validation - value in between the range
            // resolution quality 0 < x < 100
            if (sg.ResolutionQuality < 0 || sg.ResolutionQuality > 100)
            {
                MessageBox.Show("Invalid Value for resolution quality.\nValue must be between 0 to 100","Error");
                return;
            }

            // ViewDistanceQuality
            if (sg.ViewDistanceQuality < 0 || sg.ViewDistanceQuality > 5)
            {
                MessageBox.Show("Invalid Value for View Distance Quality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // AntiAliasingQuality
            if (sg.AntiAliasingQuality < 0 || sg.AntiAliasingQuality > 5)
            {
                MessageBox.Show("Invalid Value for Anti-Aliasing Quality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // ShadowQuality
            if (sg.ShadowQuality < 0 || sg.ShadowQuality > 5)
            {
                MessageBox.Show("Invalid Value for ShadowQuality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // PostProcessQuality
            if (sg.PostProcessQuality < 0 || sg.PostProcessQuality > 5)
            {
                MessageBox.Show("Invalid Value for PostProcessQuality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // TextureQuality
            if (sg.TextureQuality < 0 || sg.TextureQuality > 5)
            {
                MessageBox.Show("Invalid Value for TextureQuality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // EffectsQuality
            if (sg.EffectsQuality < 0 || sg.EffectsQuality > 5)
            {
                MessageBox.Show("Invalid Value for EffectsQuality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // PostProcessQuality
            if (sg.FoliageQuality < 0 || sg.FoliageQuality > 5)
            {
                MessageBox.Show("Invalid Value for FoliageQuality.\nValue must be between 0 to 5", "Error");
                return;
            }

            // ShadingQuality
            if (sg.ShadingQuality < 0 || sg.ShadingQuality > 5)
            {
                MessageBox.Show("Invalid Value for ShadingQuality.\nValue must be between 0 to 5", "Error");
                return;
            }


            SaveChanges(sg);

            MessageBox.Show("Settings Successfully Changed", "Done");

        }

        private void ApplyLowBtn_Click(object sender, RoutedEventArgs e)
        {
            ScalabilityGroups sg = new ScalabilityGroups
            {
                ResolutionQuality = 60,
                ViewDistanceQuality = 3,
                AntiAliasingQuality = 2,
                ShadowQuality = 3,
                PostProcessQuality = 3,
                TextureQuality = 0,
                EffectsQuality = 3,
                FoliageQuality = 3,
                ShadingQuality = 3
            };

            SaveChanges(sg);

            MessageBox.Show("Low Settings Applied.\nNB: This is the minimum quality required to play the game properly. Changes bellow this will be almost unplayable", "Done");


            ResqualityBox.Text = sg.ResolutionQuality.ToString();
            ViewDistanceQBox.Text = sg.ViewDistanceQuality.ToString();
            AntiAliasisBox.Text = sg.AntiAliasingQuality.ToString();
            ShadowQBox.Text = sg.ShadowQuality.ToString();
            PostProcessBox.Text = sg.PostProcessQuality.ToString();
            TextureQBox.Text = sg.TextureQuality.ToString();
            EffectQBox.Text = sg.EffectsQuality.ToString();
            FoliageQBox.Text = sg.FoliageQuality.ToString();
            ShadingQBox.Text = sg.ShadowQuality.ToString();
        }

        private void resDefBtn_Click(object sender, RoutedEventArgs e)
        {
            ScalabilityGroups sg = new ScalabilityGroups
            {
                ResolutionQuality = backupData.ResolutionQuality,
                ViewDistanceQuality = backupData.ViewDistanceQuality,
                AntiAliasingQuality = backupData.AntiAliasingQuality,
                ShadowQuality = backupData.ShadowQuality,
                PostProcessQuality = backupData.PostProcessQuality,
                TextureQuality = backupData.TextureQuality,
                EffectsQuality = backupData.EffectsQuality,
                FoliageQuality = backupData.FoliageQuality,
                ShadingQuality = backupData.ShadingQuality
            };

            SaveChanges(sg);

            MessageBox.Show("Default value restored.");

            ResqualityBox.Text = sg.ResolutionQuality.ToString();
            ViewDistanceQBox.Text = sg.ViewDistanceQuality.ToString();
            AntiAliasisBox.Text = sg.AntiAliasingQuality.ToString();
            ShadowQBox.Text = sg.ShadowQuality.ToString();
            PostProcessBox.Text = sg.PostProcessQuality.ToString();
            TextureQBox.Text = sg.TextureQuality.ToString();
            EffectQBox.Text = sg.EffectsQuality.ToString();
            FoliageQBox.Text = sg.FoliageQuality.ToString();
            ShadingQBox.Text = sg.ShadowQuality.ToString();
        }


        // helper method to save ini
        private void SaveChanges(ScalabilityGroups sg)
        {
            // change data
            try
            {
                for (int i = 0; i < _iniList.Count(); i++)
                {
                    _iniList[i]["ScalabilityGroups"]["sg.ResolutionQuality"] = sg.ResolutionQuality + ".000000";
                    _iniList[i]["ScalabilityGroups"]["sg.ViewDistanceQuality"] = sg.ViewDistanceQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.AntiAliasingQuality"] = sg.AntiAliasingQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.ShadowQuality"] = sg.ShadowQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.PostProcessQuality"] = sg.PostProcessQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.TextureQuality"] = sg.TextureQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.EffectsQuality"] = sg.EffectsQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.FoliageQuality"] = sg.FoliageQuality.ToString();
                    _iniList[i]["ScalabilityGroups"]["sg.ShadingQuality"] = sg.ShadingQuality.ToString();


                    var attr = File.GetAttributes(_SG[i].FilePath);

                    // unset read-only
                    attr = attr & ~FileAttributes.ReadOnly;
                    File.SetAttributes(_fnames[i], attr);

                    parser = new FileIniDataParser();
                    parser.WriteFile(_fnames[i], _iniList[i]);

                    // set read-only
                    attr = attr | FileAttributes.ReadOnly;
                    File.SetAttributes(_fnames[i], attr);
                }
            }
            catch
            {
                MessageBox.Show("Something went wrong while changing the data.");
                return;
            }


            
        }
    }
}
