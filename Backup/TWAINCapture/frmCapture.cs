using WIA;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Alfresco;
using Alfresco.RepositoryWebService;
using Alfresco.ContentWebService;
using System.IO;
using TwainDotNet;
using TwainDotNet.WinFroms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;



namespace TWAINCapture
{
    public partial class frmCapture : Form
    {
        private WIA.Device SelectedDevice;
        private string SavePath;
        private string SavedFilePath;
        private string LocationUuid;
        private string LocationName;

        string DeviceID;
        const string wiaFormatBMP = "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatPNG = "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatGIF = "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatJPEG = "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}";
        const string wiaFormatTIFF = "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}";

        private Alfresco.RepositoryWebService.Store spacesStore;
        private RepositoryService repoService;

        Twain _twain;
        ScanSettings _settings;

        public frmCapture(string sUserName, string sPassword)
        {
            InitializeComponent();
            //#region
            //AuthenticationUtils.startSession(sUserName, sPassword);
            //// Create the repo service and set the authentication credentials 创建正回购服务，并设置验证凭证
            //this.repoService = WebServiceFactory.getRepositoryService();

            //// Initialise the reference to the spaces store
            //this.spacesStore = new Alfresco.RepositoryWebService.Store();
            //this.spacesStore.scheme = Alfresco.RepositoryWebService.StoreEnum.workspace;

            //this.spacesStore.address = "SpacesStore";

            //// TWAIN Driver coding start here
            //_twain = new Twain(new WinFormsWindowMessageHook(this));
            //_twain.ScanningComplete += delegate
            //{
            //    Enabled = true;

            //    if (_twain.Images.Count > 0)
            //    {
            //        //Multi-files with single page
            //        if (chkSinglePage.Checked == false)
            //        {
            //            if (rdgif.Checked == true)
            //            {
            //                saveImages(_twain.Images, "GIF");
            //            }
            //            else if (rdjpeg.Checked == true)
            //            {
            //                saveImages(_twain.Images, "JPEG");
            //            }
            //            else
            //            {
            //                saveImages(_twain.Images, "TIFF");
            //            }
            //            //Single file with Multi-pages
            //        }
            //        else
            //        {

            //            saveMultipage(_twain.Images, "TIFF");
            //        }

            //        _twain.Images.Clear();
            //    }
            //};
            //#endregion
            //TWAIN Coding End here
        }

        public void saveImages(IList<Image> bmp, string type)
        {

            Random RandomClass = new Random();
           //Loop in all images and save them on separate files
            int counter;
            for (int i = 0; i < bmp.Count; i++)
            {


                counter = RandomClass.Next(1000);
              
                string file = SavePath + "\\";
                file += txtPrefix.Text;
                file += i + ".";
                file += type;

                if (System.IO.File.Exists(file))
                {
           

                    counter++;

                    file = SavePath + "\\";
                    file += txtPrefix.Text;
                    file += counter + ".";
                    file += type;

                }

                string filename = txtPrefix.Text;
                filename += counter + ".";
                filename += type;

              
                bmp[i].Save(file);
                // Upload file to Alfresco
                UploadNow(filename, file);
                MessageBox.Show(filename + " uploaded");
            }
               
        }

        private void frmCapture_Load(object sender, EventArgs e)
        {
              //sets savepath to application directory
             // SavePath = Application.StartupPath;
            //initializeRootFolder();
        }
        /// <summary>
        /// 扫描仪器
        /// </summary>
        private void GetDevice()
        {
            // This will show the select device dialog to choose which device to use
            try
            {

             CommonDialogClass MyDialog = new CommonDialogClass();
            //Device MyDevice = MyDialog.ShowSelectDevice(WiaDeviceType.UnspecifiedDeviceType, false, true);
             Device MyDevice = null;

            if (rdothers.Checked == true)
            {
                 MyDevice = MyDialog.ShowSelectDevice(WiaDeviceType.UnspecifiedDeviceType, false, true);
            }
            //else if (MyDevice.Type == WIA.WiaDeviceType.CameraDeviceType)
            else if (rdothers.Checked == true)
            {
                MyDevice = MyDialog.ShowSelectDevice(WIA.WiaDeviceType.CameraDeviceType, false, true);
               // rdCamera.Checked = true;
            }
           
              if ((MyDevice != null))
                {
                    //loops through device properties, only gets the ones we want to display
                    foreach (WIA.Property prop in MyDevice.Properties)
                    {
                        switch (prop.Name)
                        {
                            case "Manufacturer":
                                lblMfg.Text =Convert.ToString(prop.get_Value());
                                break;
                            case "Description":
                                lblDesc.Text = Convert.ToString(prop.get_Value());
                                break;
                            case "Name":
                                lblName.Text = Convert.ToString(prop.get_Value());
                                break;
                            case "WIA Version":
                                lblWIA.Text = Convert.ToString(prop.get_Value());
                                break;
                            case "Driver Version":
                                lblDriver.Text = Convert.ToString(prop.get_Value());

                                break;
                        }
                    }
                    //sets MyDevice form level selected device
                    SelectedDevice = MyDevice;
                    btncapture.Enabled = true;
                }
                else
                {
                    lblName.Text = "No WIA Devices Found!";
                  MessageBox.Show("No WIA Devices Found!");
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show("Problem! " + ex.Message, "Problem Loading Device", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                
            }
        }

        private void UploadNow(string fileName, string file)
        {


            // Initialise the reference to the spaces store
            Alfresco.RepositoryWebService.Store spacesStore = new Alfresco.RepositoryWebService.Store();
            spacesStore.scheme = Alfresco.RepositoryWebService.StoreEnum.workspace;
            spacesStore.address = "SpacesStore";

            // Create the parent reference, the company home folder
            Alfresco.RepositoryWebService.ParentReference parentReference = new Alfresco.RepositoryWebService.ParentReference();
            parentReference.store = spacesStore;
            //parentReference.path = "/app:company_home"
            parentReference.uuid = LocationUuid;
            parentReference.associationType = Constants.ASSOC_CONTAINS;
            parentReference.childName = Constants.createQNameString(Constants.NAMESPACE_CONTENT_MODEL, fileName);

            // Create the properties list
            NamedValue nameProperty = new NamedValue();
            nameProperty.name = Constants.PROP_NAME;
            nameProperty.value = fileName;
            nameProperty.isMultiValue = false;

            NamedValue[] properties = new NamedValue[2];
            properties[0] = nameProperty;
            nameProperty = new NamedValue();
            nameProperty.name = Constants.PROP_TITLE;
            nameProperty.value = fileName;
            nameProperty.isMultiValue = false;
            properties[1] = nameProperty;

            // Create the CML create object
            CMLCreate create = new CMLCreate();
            create.parent = parentReference;
            create.id = "1";
            create.type = Constants.TYPE_CONTENT;
            create.property = properties;

            // Create and execute the cml statement
            CML cml = new CML();
            cml.create = new CMLCreate[] { create };
            UpdateResult[] updateResult = repoService.update(cml);

            // work around to cast Alfresco.RepositoryWebService.Reference to
            // Alfresco.ContentWebService.Reference 
            Alfresco.RepositoryWebService.Reference rwsRef = updateResult[0].destination;
            Alfresco.ContentWebService.Reference newContentNode = new Alfresco.ContentWebService.Reference();
            newContentNode.path = rwsRef.path;
            newContentNode.uuid = rwsRef.uuid;
            Alfresco.ContentWebService.Store cwsStore = new Alfresco.ContentWebService.Store();
            cwsStore.address = "SpacesStore";
            spacesStore.scheme = Alfresco.RepositoryWebService.StoreEnum.workspace;
            newContentNode.store = cwsStore;

            // Open the file and convert to byte array 
            FileStream inputStream = new FileStream(file, FileMode.Open);

            int bufferSize = (int)inputStream.Length;
            byte[] bytes = new byte[bufferSize];
            inputStream.Read(bytes, 0, bufferSize);

            inputStream.Close();

            ContentFormat contentFormat = new ContentFormat();
            if (rdjpeg.Checked == true)
            {
                contentFormat.mimetype = "image/jpeg";
            }
            else if (rdgif.Checked == true)
            {
                contentFormat.mimetype = "image/gif";
            }
            else if (rdtiff.Checked == true)
            {
                contentFormat.mimetype = "image/tiff";
            }

            WebServiceFactory.getContentService().write(newContentNode, Constants.PROP_CONTENT, bytes, contentFormat);
        }

        private void buildTree(TreeNode parentNode, Alfresco.RepositoryWebService.Reference childReference)
        {

            // Query for the children of the reference
            QueryResult result = this.repoService.queryChildren(childReference);
            if (result.resultSet.rows != null)
            {
                foreach (ResultSetRow row in result.resultSet.rows)
                {
                    // only interested in folders
                    if (row.node.type.Contains("folder") == true)
                    {
                        foreach (NamedValue namedValue in row.columns)
                        {
                            if (namedValue.name.Contains("name") == true)
                            {
                                // add a node to the tree view
                                TreeNode node = this.addChildNode(parentNode, namedValue.value, row.node);

                                // Create the reference for the node selected
                                Alfresco.RepositoryWebService.Reference reference = new Alfresco.RepositoryWebService.Reference();
                                reference.store = this.spacesStore;
                                reference.uuid = row.node.id;
                                // add the child nodes
                                buildTree(node, reference);
                            }
                        }
                    }
                }
            }
        }

        private void initializeRootFolder()
        {

            // Suppress repainting the TreeView until all the objects have been created.
            treealfresco.BeginUpdate();

            // get the root position, Company Home

            TreeNode rootNode = new TreeNode();

            Alfresco.RepositoryWebService.Reference reference = new Alfresco.RepositoryWebService.Reference();
            reference.store = this.spacesStore;
            reference.path = "/app:company_home";

            // Create a query object
            Alfresco.RepositoryWebService.Query query = new Alfresco.RepositoryWebService.Query();
            query.language = Alfresco.RepositoryWebService.QueryLanguageEnum.lucene;
            query.statement = "Path:\"/\" AND @cm\\:title:\"Company Home\"";

            QueryResult result = this.repoService.query(this.spacesStore, query, true);
            string name = null;
            if (result.resultSet.rows != null)
            {
                // construct root node
                ResultSetRow row = result.resultSet.rows[0];
                foreach (NamedValue namedValue in row.columns)
                {
                    if (namedValue.name.Contains("title") == true)
                    {
                        name = namedValue.value;
                        rootNode.Text = name;
                        rootNode.Name = name;
                    }
                }
                rootNode.Tag = row.node;
            }
            // add the root node to the tree view
            this.treealfresco.Nodes.AddRange(new System.Windows.Forms.TreeNode[] { rootNode });

            buildTree(rootNode, reference);

            // Begin repainting the TreeView.

            treealfresco.EndUpdate();
        }

        private TreeNode addChildNode(TreeNode parentNode, string name, ResultSetRowNode rsrNode)
        {

            TreeNode node = new TreeNode(name);
            node.Text = name;
            node.Tag = rsrNode;
            parentNode.Nodes.Add(node);
            return node;
        }

        private void rdCamera_CheckedChanged(object sender, EventArgs e)
        {
            if (rdCamera.Checked == true)
            {
                rdjpeg.Enabled = true;
                rdjpeg.Checked = true;
                rdgif.Enabled = true;
                rdtiff.Enabled = false;
                btnselectdevice.Enabled = true;
                useAdfCheckBox.Enabled = false;
                useUICheckBox.Enabled = false;
                selectADeviceToolStripMenuItem.Enabled = true;
            }
        }

        private void rdscanner_CheckedChanged(object sender, EventArgs e)
        {
            if (rdscanner.Checked == true)
            {
                rdjpeg.Enabled = true;
                rdgif.Enabled = true;
                rdtiff.Enabled = true;
                rdtiff.Checked = true;
                btnselectdevice.Enabled = true;
                useAdfCheckBox.Enabled = true;
                useUICheckBox.Enabled = true;
                           selectADeviceToolStripMenuItem.Enabled = true;
                
            }
        }

        private void rdVideo_CheckedChanged(object sender, EventArgs e)
        {
            if (rdVideo.Checked == true)
            {
                rdjpeg.Enabled = false;
                rdgif.Enabled = false;
                rdtiff.Enabled = false;
                useAdfCheckBox.Enabled = false;
                useUICheckBox.Enabled = false;
                MessageBox.Show("Coming soon", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void rdothers_CheckedChanged(object sender, EventArgs e)
        {
            if (rdothers.Checked == true)
            {
                rdjpeg.Enabled = true;
                rdjpeg.Checked = true;
                rdgif.Enabled = true;
                rdtiff.Enabled = false;
                btnselectdevice.Enabled = true;
                useAdfCheckBox.Enabled = false;
                useUICheckBox.Enabled = false;
                selectADeviceToolStripMenuItem.Enabled = true;
            }
        }

        private void btnselectdevice_Click(object sender, EventArgs e)
        {
            //扫描
            if (rdscanner.Checked == true)
            {
                _twain.SelectSource();
            }
            else
            {
                GetDevice();
            }
        }

        private void btncapture_Click(object sender, EventArgs e)
        {
            if (treealfresco.SelectedNode == null)
            {
                MessageBox.Show("Please select a Space to store your data into", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
             //exit btncapture_Click;
            }

            if (rdscanner.Checked == true)
            {
                Enabled = false;

                _settings = new ScanSettings()
                {
                    UseDocumentFeeder = useAdfCheckBox.Checked,
                    ShowTwainUI = useUICheckBox.Checked,
                    Resolution = blackAndWhiteCheckBox.Checked
                        ? ResolutionSettings.Fax
                        : ResolutionSettings.ColourPhotocopier
                };

                try
                {
                    _twain.StartScanning(_settings);
                }
                catch (TwainException ex)
                {
                    MessageBox.Show(ex.Message);
                    Enabled = true;
                }
            }
            else
            {
                WIA.Item item = default(WIA.Item);
                WIA.CommonDialog WiaCommonDialog = new CommonDialogClass();
                try
                {
                    //Check if the device is scanner or not
                    if (rdscanner.Checked == true)
                    {
                        //scans the image using the Scanner only (ADF or Flatbed)
                        DoScan();
                    }
                    else
                    {
                        item = SelectedDevice.ExecuteCommand(WIA.CommandID.wiaCommandTakePicture);

                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("Problem Taking Picture. Please make sure that the camera is plugged in and is not in use by another application. " + "\r\n" + "Extra Info:" + ex.Message, "Problem Grabbing Picture", MessageBoxButtons.OK, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    return;
                }

                try
                {
                    //Validate if a image location is selected or not
                    if (treealfresco.SelectedNode == null)
                    {
                        MessageBox.Show("Please select a Space to store your data into", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    ResultSetRowNode node = (ResultSetRowNode)treealfresco.SelectedNode.Tag;
                    LocationUuid = node.id;

                    LocationName = treealfresco.SelectedNode.Text;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

                try
                {
                    //Executes the device's TakePicture command based on selected image format
                    if (rdjpeg.Checked == true)
                    {
                        string jpegGuid = null;
                        //retrieves jpegKey from registry, used in saving JPEG
                        Microsoft.Win32.RegistryKey jpegKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\{D2923B86-15F1-46FF-A19A-DE825F919576}\\SupportedExtension\\.jpg");
                        jpegGuid = (string)jpegKey.GetValue("FormatGUID");
                        //loops through available formats for the captured item, looking for the JPG format
                        foreach (string format in item.Formats)
                        {
                            if ((format == jpegGuid))
                            {
                                //transfers image to an imagefile object
                                WIA.ImageFile imagefile = (WIA.ImageFile)item.Transfer(format);
                                int Counter = 0;
                                //counter in loop appended to filename 
                                bool LoopAgain = true;
                                //searches directory, gets next available picture name
                                while (!(LoopAgain == false))
                                {
                                         string File = SavePath + "\\";
                                       File += txtPrefix.Text;
                                       File += Counter;
                                       File+= ".jpg";
                                       string Filename = txtPrefix.Text;
                                       Filename += Counter;
                                          Filename +=".jpg";
                                    //if file doesnt exist, save the file
                                    if (!System.IO.File.Exists(Filename))
                                    {
                                        SavedFilePath = Filename;
                                        imagefile.SaveFile(Filename);
                                        //saves file to disk
                                        //Upload the file to Alfresco
                                        UploadNow(Filename, File);
                                        MessageBox.Show(File + " uploaded");
                                        LoopAgain = false;
                                    }
                                    Counter = Counter + 1;
                                }
                            }
                        }
                    }
                    else if (rdgif.Checked == true)
                    {
                        string jpegGuid = null;
                        //retrieves jpegKey from registry, used in saving JPEG
                        Microsoft.Win32.RegistryKey jpegKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\{D2923B86-15F1-46FF-A19A-DE825F919576}\\SupportedExtension\\.jpg");
                        jpegGuid = (string)jpegKey.GetValue("FormatGUID");
                        //loops through available formats for the captured item, looking for the JPG format
                        foreach (string format in item.Formats)
                        {
                            if ((format == jpegGuid))
                            {
                                //transfers image to an imagefile object
                                WIA.ImageFile imagefile = (WIA.ImageFile)item.Transfer(WIA.FormatID.wiaFormatGIF);
                                int Counter = 0;
                                //counter in loop appended to filename 
                                bool LoopAgain = true;
                                //searches directory, gets next available picture name
                                while (!(LoopAgain == false))
                                {
                                    string File = SavePath + "\\";
                                    File += txtPrefix.Text;
                                    File += Counter;
                                    File += ".gif";
                                    string Filename = txtPrefix.Text;
                                    Filename += Counter;
                                    Filename += ".gif";
                                    //if file doesnt exist, save the file
                                    if (!System.IO.File.Exists(Filename))
                                    {
                                        SavedFilePath = Filename;
                                        imagefile.SaveFile(Filename);
                                        //saves file to disk
                                        // Upload the file to Alfresco
                                        UploadNow(Filename, File);
                                        MessageBox.Show(File + " uploaded");
                                        LoopAgain = false;
                                    }
                                    Counter = Counter + 1;
                                }
                            }
                        }
                    }
                    else if (rdtiff.Checked == true)
                    {
                        string jpegGuid = null;
                        //retrieves jpegKey from registry, used in saving JPEG
                        Microsoft.Win32.RegistryKey jpegKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\{D2923B86-15F1-46FF-A19A-DE825F919576}\\SupportedExtension\\.jpg");
                        jpegGuid = (string)jpegKey.GetValue("FormatGUID");
                        //loops through available formats for the captured item, looking for the JPG format
                        foreach (string format in item.Formats)
                        {
                            if ((format == jpegGuid))
                            {
                                //transfers image to an imagefile object
                                WIA.ImageFile imagefile = (WIA.ImageFile)item.Transfer(WIA.FormatID.wiaFormatTIFF);
                                int Counter = 0;
                                //counter in loop appended to filename 
                                bool LoopAgain = true;
                                //searches directory, gets next available picture name
                                while (!(LoopAgain == false))
 
                               {
                                   string File = SavePath + "\\";
                                   File += txtPrefix.Text;
                                   File += Counter;
                                   File += ".tiff";
                                   string Filename = txtPrefix.Text;
                                   Filename += Counter;
                                   Filename += ".tiff";
                                    //if file doesnt exist, save the file
                                    if (!System.IO.File.Exists(Filename))
                                    {
                                        SavedFilePath = Filename;
                                        imagefile.SaveFile(Filename);
                                        //saves file to disk
                                        UploadNow(Filename, File);
                                        MessageBox.Show(File + " uploaded");
                                        LoopAgain = false;
                                    }
                                    Counter = Counter + 1;
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
     
        private void DoScan()
        {

            WIA.Item item = default(WIA.Item);
            WIA.ImageFile Img = default(WIA.ImageFile);
            WIA.CommonDialog WiaCommonDialog = new CommonDialogClass();
            bool hasMorePages = true;
            int x = 0;
            int numPages = 0;
            string ImgMain = null;
            string ImgMainName = null;

            try
            {
                if (treealfresco.SelectedNode == null)
                {
                    MessageBox.Show("Please select a Space to store your data into", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ResultSetRowNode node = (ResultSetRowNode)treealfresco.SelectedNode.Tag;
                LocationUuid = node.id;

                LocationName = treealfresco.SelectedNode.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            while (hasMorePages)
            {
                //Create DeviceManager
                DeviceManager manager = new DeviceManagerClass();
                Device WiaDev = null;
                foreach (DeviceInfo info in manager.DeviceInfos)
                {
                    if (info.DeviceID == this.DeviceID)
                    {
                        WIA.Properties infoprop = null;
                        infoprop = info.Properties;
                        //connect to scanner
                        WiaDev = info.Connect();
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }

                //Start Scan

                Img = null;

                item = WiaDev.Items[1] as WIA.Item;
                
                try
                {
                    Img = (ImageFile)WiaCommonDialog.ShowTransfer(item, wiaFormatTIFF, false);

                    //process image:
                    //one would do image processing here

                    //Save to file
                    string jpegGuid = null;
                    //retrieves jpegKey from registry, used in saving JPEG
                    Microsoft.Win32.RegistryKey jpegKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey("CLSID\\{D2923B86-15F1-46FF-A19A-DE825F919576}\\SupportedExtension\\.jpg");
                    jpegGuid = (string)jpegKey.GetValue("FormatGUID");
                    //loops through available formats for the captured item, looking for the JPG format
                    foreach (string format in item.Formats)
                    {
                        if ((format == jpegGuid))
                        {
                            //transfers image to an imagefile object
                            Img = (WIA.ImageFile)item.Transfer(WIA.FormatID.wiaFormatTIFF);
                            int Counter = 0;
                            //counter in loop appended to filename 
                            bool LoopAgain = true;
                            //searches directory, gets next available picture name
                            while (!(LoopAgain == false))
                            {
                                string File = SavePath + "\\";
                                       File += txtPrefix.Text;
                                       File += Counter;
                                       File+= ".tiff";
                                       string Filename = txtPrefix.Text;
                                       Filename += Counter;
                                          Filename +=".tiff";


                                if (System.IO.File.Exists(Filename))
                                {
                                    //file exists, delete it
                                    System.IO.File.Delete(Filename);
                                }
                                if (numPages == 0)
                                {
                                    Img.SaveFile(Filename);
                                }
                                ImgMain = File;
                                ImgMainName = Filename;
                                if (numPages > 0)
                                {
                                    int y = Counter;
                                    y = Counter - 1;
                                    ImgMain = SavePath + "\\" ;
                                    ImgMain+= txtPrefix.Text;
                                    ImgMain+= y; 
                                    ImgMain+= ".tiff";
                                    ImgMainName = txtPrefix.Text;
                                    ImgMainName += y; 
                                    ImgMainName +=".tiff";
                                    //createtiff(ImgMain, File);
                                }
                                numPages += 1;
                                                              
                                Counter = Counter + 1;
                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
                finally
                {
                    item = null;
                    //determine if there are any more pages waiting
                    WIA.Property documentHandlingSelect = null;
                    WIA.Property documentHandlingStatus = null;
                    foreach (WIA.Property prop in WiaDev.Properties)
                    {
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_SELECT)
                        {
                            documentHandlingSelect = prop;
                        }
                        if (prop.PropertyID == WIA_PROPERTIES.WIA_DPS_DOCUMENT_HANDLING_STATUS)
                        {
                            documentHandlingStatus = prop;
                        }
                    }
                    hasMorePages = false;
                    UploadNow(ImgMainName, ImgMain);
                    MessageBox.Show(ImgMain + " uploaded");
                    //assume there are no more pages
                    if (documentHandlingSelect != null)
                    {
                        //may not exist on flatbed scanner but required for feeder
                        //check for document feeder
                        
                        if ((Convert.ToUInt32(documentHandlingSelect.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_SELECT.FEEDER) != 0)
                        {
                            hasMorePages = ((Convert.ToUInt32(documentHandlingStatus.get_Value()) & WIA_DPS_DOCUMENT_HANDLING_STATUS.FEED_READY) != 0);
                        }
                    }
                    x += 1;
                }
            }
        }

        private void selectADeviceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (rdscanner.Checked == true)
            {
                _twain.SelectSource();
            }
            else
            {
                GetDevice();
            }
        }

        private void blackAndWhiteCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void aboutCapturescoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form frm = new About_cap();
            frm.ShowDialog();
      

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void saveMultipage(IList<Image> bmp, string type)
        {

            string location = SavePath + "\\";
            location += txtPrefix.Text;
            location += ".";
            location += type;

            if (bmp != null)
            {
                try
                {
                    ImageCodecInfo codecInfo = getCodecForstring(type);

                    for (int i = 0; i < bmp.Count; i++)
                    {
                        if (bmp[i] == null)
                            break;
                        bmp[i] = (Image)ConvertToBitonal((Bitmap)bmp[i]);
                    }

                    if (bmp.Count == 1)
                    {

                        EncoderParameters iparams = new EncoderParameters(1);
                        System.Drawing.Imaging.Encoder iparam = System.Drawing.Imaging.Encoder.Compression;
                        EncoderParameter iparamPara = new EncoderParameter(iparam, (long)(EncoderValue.CompressionCCITT4));
                        iparams.Param[0] = iparamPara;
                        bmp[0].Save(location, codecInfo, iparams);

                    }
                    else if (bmp.Count > 1)
                    {

                        System.Drawing.Imaging.Encoder saveEncoder;
                        System.Drawing.Imaging.Encoder compressionEncoder;
                        EncoderParameter SaveEncodeParam;
                        EncoderParameter CompressionEncodeParam;
                        EncoderParameters EncoderParams = new EncoderParameters(2);

                        saveEncoder = System.Drawing.Imaging.Encoder.SaveFlag;
                        compressionEncoder = System.Drawing.Imaging.Encoder.Compression;

                        // Save the first page (frame).
                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.MultiFrame);
                        CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                        EncoderParams.Param[0] = CompressionEncodeParam;
                        EncoderParams.Param[1] = SaveEncodeParam;

                        File.Delete(location);

                        bmp[0].Save(location, codecInfo, EncoderParams);

                        for (int i = 1; i < bmp.Count; i++)
                        {
                            
                            if (bmp[i] == null)
                                break;

                            SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.FrameDimensionPage);
                            CompressionEncodeParam = new EncoderParameter(compressionEncoder, (long)EncoderValue.CompressionCCITT4);
                            EncoderParams.Param[0] = CompressionEncodeParam;
                            EncoderParams.Param[1] = SaveEncodeParam;
                            bmp[0].SaveAdd(bmp[i], EncoderParams);

                        }

                        SaveEncodeParam = new EncoderParameter(saveEncoder, (long)EncoderValue.Flush);
                        EncoderParams.Param[0] = SaveEncodeParam;
                        bmp[0].SaveAdd(EncoderParams);
                    }
         
                }
                catch (System.Exception ee)
                {
                    throw new Exception(ee.Message + "  Error in saving as multipage ");
                }
            }
       
        }

        private ImageCodecInfo getCodecForstring(string type)
        {
            ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();

            for (int i = 0; i < info.Length; i++)
            {
                string EnumName = type.ToString();
                if (info[i].FormatDescription.Equals(EnumName))
                {
                    return info[i];
                }
            }

            return null;

        }

        public Bitmap ConvertToBitonal(Bitmap original)
        {
            Bitmap source = null;

            // If original bitmap is not already in 32 BPP, ARGB format, then convert
            if (original.PixelFormat != PixelFormat.Format32bppArgb)
            {
                source = new Bitmap(original.Width, original.Height, PixelFormat.Format32bppArgb);
                source.SetResolution(original.HorizontalResolution, original.VerticalResolution);
                using (Graphics g = Graphics.FromImage(source))
                {
                    g.DrawImageUnscaled(original, 0, 0);
                }
            }
            else
            {
                source = original;
            }

            // Lock source bitmap in memory
            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            // Copy image data to binary array
            int imageSize = sourceData.Stride * sourceData.Height;
            byte[] sourceBuffer = new byte[imageSize];
            Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, imageSize);

            // Unlock source bitmap
            source.UnlockBits(sourceData);

            // Create destination bitmap
            Bitmap destination = new Bitmap(source.Width, source.Height, PixelFormat.Format1bppIndexed);

            // Lock destination bitmap in memory
            BitmapData destinationData = destination.LockBits(new Rectangle(0, 0, destination.Width, destination.Height), ImageLockMode.WriteOnly, PixelFormat.Format1bppIndexed);

            // Create destination buffer
            imageSize = destinationData.Stride * destinationData.Height;
            byte[] destinationBuffer = new byte[imageSize];

            int sourceIndex = 0;
            int destinationIndex = 0;
            int pixelTotal = 0;
            byte destinationValue = 0;
            int pixelValue = 128;
            int height = source.Height;
            int width = source.Width;
            int threshold = 500;

            // Iterate lines
            for (int y = 0; y < height; y++)
            {
                sourceIndex = y * sourceData.Stride;
                destinationIndex = y * destinationData.Stride;
                destinationValue = 0;
                pixelValue = 128;

                // Iterate pixels
                for (int x = 0; x < width; x++)
                {
                    // Compute pixel brightness (i.e. total of Red, Green, and Blue values)
                    pixelTotal = sourceBuffer[sourceIndex + 1] + sourceBuffer[sourceIndex + 2] + sourceBuffer[sourceIndex + 3];
                    if (pixelTotal > threshold)
                    {
                        destinationValue += (byte)pixelValue;
                    }
                    if (pixelValue == 1)
                    {
                        destinationBuffer[destinationIndex] = destinationValue;
                        destinationIndex++;
                        destinationValue = 0;
                        pixelValue = 128;
                    }
                    else
                    {
                        pixelValue >>= 1;
                    }
                    sourceIndex += 4;
                }
                if (pixelValue != 128)
                {
                    destinationBuffer[destinationIndex] = destinationValue;
                }
            }

            // Copy binary image data to destination bitmap
            Marshal.Copy(destinationBuffer, 0, destinationData.Scan0, imageSize);

            // Unlock destination bitmap
            destination.UnlockBits(destinationData);

            // Return
            return destination;
        }

        private void rdtiff_CheckedChanged(object sender, EventArgs e)
        {
            btncapture.Enabled = true;

        }

        private void rdgif_CheckedChanged(object sender, EventArgs e)
        {
            btncapture.Enabled = true;

        }

        private void rdjpeg_CheckedChanged(object sender, EventArgs e)
        {
            btncapture.Enabled = true;

        }

        private void chkSinglePage_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSinglePage.Checked == true)
            {
                rdjpeg.Enabled = false;
                rdgif.Enabled = false;
                rdtiff.Checked = true;
            }
            else
            {
                rdjpeg.Enabled = true;
                rdgif.Enabled = true;
                

            }

        }

        private void treealfresco_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ResultSetRowNode node = (ResultSetRowNode)treealfresco.SelectedNode.Tag;
            LocationUuid = node.id;
            LocationName = treealfresco.SelectedNode.Text;

        }

        private void useAdfCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (useAdfCheckBox.Checked == true)
            {
                chkSinglePage.Enabled = true;
            }
            else {

                chkSinglePage.Enabled = false;
            }
            

        }
    }
}
