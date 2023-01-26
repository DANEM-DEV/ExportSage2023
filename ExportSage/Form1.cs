using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data.Odbc;
using System.Globalization;
using Microsoft.Win32;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Net;

using System.Runtime.InteropServices;

namespace ExportSage
{
    public partial class Form1 : Form
    {

        string m_stODBC;
        public Form1(string stODBC)
        {
            //ODBCManager.CreateDSN(GiveFld(stODBC, 0, ","), "", GiveFld(stODBC, 3, ","), "SQL Server", true, GiveFld(stODBC, 0, ","));
            m_stODBC = stODBC;
            //string stConnString = "dsn=" + GiveFld(stODBC, 0, ",") + ";UID=" + GiveFld(stODBC, 1, ",") + ";PWD=" + GiveFld(stODBC, 2, ",")+";MultipleActiveResultSets=True";


            InitializeComponent();
        }
        public static OdbcConnection odbc_connexion = null;
        public static OdbcConnection odbc_connexion2 = null;
        static private TextWriter m_fileENT;
        static private TextWriter m_fileLIN;
        static private TextWriter m_Log = null;
        static private bool GL_bNoFlag = false;
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                IniFile MyIni = new IniFile("exportsage.ini");


                /*var ODBC_ini = MyIni.Read("ODBC");
                var PathExport_ini = MyIni.Read("PathExport");
                var societe_ini = MyIni.Read("societe");
                var separator_ini = MyIni.Read("separator");
                var vu_ent_ini = MyIni.Read("vu_ent");
                var vu_lin_ini = MyIni.Read("vu_lin");
                var hdr_ent_ini = MyIni.Read("hdr_ent");
                var hdr_lin_ini = MyIni.Read("hdr_lin");
                var prefixe_file_ent_ini = MyIni.Read("prefixe_file_ent");
                var prefixe_file_lin_ini = MyIni.Read("prefixe_file_lin");
                var sufixe_file_ini = MyIni.Read("sufixe_file");
                var type_ent_ini = MyIni.Read("type_ent");
                var type_lin_ini = MyIni.Read("type_lin");
                var nolog_ini = MyIni.Read("nolog");
                var noflag_ini = MyIni.Read("noflag");*/

                string ODBC_ini = MyIni.GetValue("ExportSage", "ODBC");
                string PathExport_ini = MyIni.GetValue("ExportSage", "PathExport" );
                string updateflag_ini = MyIni.GetValue("ExportSage", "updateflag");
                string andflag_ini = MyIni.GetValue("ExportSage", "andflag");
                string separator_ini = MyIni.GetValue("ExportSage", "separator");
                string vu_ent_ini = MyIni.GetValue("ExportSage", "vu_ent");
                string vu_lin_ini = MyIni.GetValue("ExportSage", "vu_lin");
                string order_ent_ini = MyIni.GetValue("ExportSage", "order_ent");       //ajout 220602
                string order_lin_ini = MyIni.GetValue("ExportSage", "order_lin");       //ajout 220602    
                string hdr_ent_ini = MyIni.GetValue("ExportSage", "hdr_ent");
                string hdr_lin_ini = MyIni.GetValue("ExportSage", "hdr_lin");
                string prefixe_file_ent_ini = MyIni.GetValue("ExportSage", "prefixe_file_ent");
                string prefixe_file_lin_ini = MyIni.GetValue("ExportSage", "prefixe_file_lin");
                string sufixe_file_ini = MyIni.GetValue("ExportSage", "sufixe_file");
                string type_ent_ini = MyIni.GetValue("ExportSage", "type_ent");
                string type_lin_ini = MyIni.GetValue("ExportSage", "type_lin");
                string nolog_ini = MyIni. GetValue("ExportSage", "nolog");
                string noflag_ini = MyIni.GetValue("ExportSage", "noflag");
                string keyappalive_ini = MyIni.GetValue("ExportSage", "keyappalive");
                string replace_idx01_in_flag_query_ini = MyIni.GetValue("ExportSage", "replace_idx01_in_flag_query");


                //valeurs par defaut des var_ini
                string stConnString = "DRIVER={SQL Server};SERVER=localhost;DATABASE=negos_CGA;UID=negos_CGA;PWD=FlRtNpj4llJj";
                stConnString = controlINI(ODBC_ini, stConnString);
                string pathExport = "C:\\danem\\negos\\export\\";
                pathExport = controlINI(PathExport_ini, pathExport);
                string updateflag = "";
                updateflag = controlINI(updateflag_ini, updateflag);        //ajout 15/10 du ce param pour gerer le champs et la valeur maj  lors du flaggage (valeur par defaut 'flag='5')
                string andflag = "";
                andflag = controlINI(andflag_ini, andflag);
                string separator = ";";
                separator = controlINI(separator_ini, separator);
                if (separator == "tabulation")
                    separator = "\t";
                string vu_ent = "";
                vu_ent = controlINI(vu_ent_ini, vu_ent);
                string vu_lin = "";
                vu_lin = controlINI(vu_lin_ini, vu_lin);
                //ajout 220602    
                string order_ent = "";
                order_ent = controlINI(order_ent_ini, order_ent);
                string order_lin = "";
                order_lin = controlINI(order_lin_ini, order_lin);

                string hdr_ent = "";
                hdr_ent = controlINI(hdr_ent_ini, hdr_ent);
                string hdr_lin = "";
                hdr_lin = controlINI(hdr_lin_ini, hdr_lin);
                string prefixe_file_ent = "hdr-";
                prefixe_file_ent = controlINI(prefixe_file_ent_ini, prefixe_file_ent);
                string prefixe_file_lin = "lin-";
                prefixe_file_lin = controlINI(prefixe_file_lin_ini, prefixe_file_lin);
                string sufixe_file = ".txt";
                sufixe_file = controlINI(sufixe_file_ini, sufixe_file);
                string type_ent = "";
                type_ent = controlINI(type_ent_ini, type_ent);
                string type_lin = "";
                type_lin = controlINI(type_lin_ini, type_lin);
                string keyappalive = "";
                keyappalive = controlINI(keyappalive_ini, keyappalive);
                bool nolog = false;
                if (nolog_ini != null)
                    if (nolog_ini.ToString() == "true")
                        nolog = true;
                if (noflag_ini != null)
                    if (noflag_ini.ToString() == "true")
                        GL_bNoFlag = true;
                string replace_idx01_in_flag_query = "";
                replace_idx01_in_flag_query = controlINI(replace_idx01_in_flag_query_ini, replace_idx01_in_flag_query);


                //stConnString = "DRIVER={SQL Server};SERVER=SD3;DATABASE=negos_CGA;UID=negos_CGA;PWD=FlRtNpj4llJj";      //debug
                string stPathLog;
                if(nolog == false)
                {
                    //Remontée du log ici
                    stPathLog = Directory.GetCurrentDirectory() + "\\logsage.txt";
                    m_Log = new StreamWriter(stPathLog, true, System.Text.Encoding.Default);

                }
                Log("Form1_Load: ");
                Log(stConnString);


                labelConnString.Text = stConnString;

                //overture de la base de données
                odbc_connexion = new OdbcConnection(stConnString);
                odbc_connexion.Open();
                Log("Open...");

                odbc_connexion2 = new OdbcConnection(stConnString);
                odbc_connexion2.Open();
                Log("Open2...");

                DeleteFile(pathExport, "*.tmp");

                string stLine = "", stFld = "";
                string stLineComment;
                int nCountArt = 0;
                string stQueryLin;
                ArrayList ListCmdtoFlag = new ArrayList();

                //Generation d'un fichier par commande
                //on boucle tant qu'il y a des commande à faire, avec une attente de 2s entre chaque commandes, pour etre sur que que chaque fichier est unique
                bool bCmdExported = false;      //utilisation de ce bool pour conditionner le deuxième export (rapport)
                string stQueryHdr;
                string HORODATAGE = "";
                HORODATAGE = DateTime.Now.ToString("yyyy/MM/dd/HH/mm/ss");     //ajout des secondes
                HORODATAGE = HORODATAGE.Replace("/", "");
                HORODATAGE = HORODATAGE.Replace(":", "");
                HORODATAGE = HORODATAGE.Replace(" ", "");

                if (true)  //Pour reset les variable entre les 2 traitements
                {

                    OdbcCommand m_myCommandPropUpdate = new OdbcCommand("", odbc_connexion);
                    string stPathEnt;
                    stPathEnt = "";
                    //do 
                    //{
                    //    bCmdExported = false;
                    stQueryHdr = @"SELECT * FROM  "+ vu_ent+" "+order_ent;


                    OdbcCommand m_myCommandPropHdr = new OdbcCommand(stQueryHdr, odbc_connexion);
                    OdbcDataReader drCompoPropHdr = m_myCommandPropHdr.ExecuteReader();



                    int nNbrCde = 0;
                    string stDateFile = "";
                    string oldCde = "";
                    if (drCompoPropHdr.Read())
                    {
                        bCmdExported = true;
                        Log("Hdr1");
                        //string stCurentRep = drCompoPropHdr["rep_nom"].ToString() ;
                        int iIdxFile = 1;

                        Log("Open " + stPathEnt);

                        do
                        {
                            stLineComment = "";
                            nNbrCde++;
                            if (oldCde != drCompoPropHdr["numcde"].ToString())
                            {
                                ListCmdtoFlag.Add(drCompoPropHdr["numcde"].ToString());
                                oldCde = drCompoPropHdr["numcde"].ToString();
                                if (m_fileENT != null)
                                    m_fileENT.Close();
                                //stPath = pathExport + "\\devis-" + drCompoPropHdr["ENTCDE_CODECDE"].ToString() + "-" + stPath + ".tmp";
                                stPathEnt = pathExport + "\\" + prefixe_file_ent + HORODATAGE +"_"+ drCompoPropHdr["numcde"].ToString() + ".tmp";
                                //iCpt++;


                                m_fileENT = new StreamWriter(stPathEnt, false, System.Text.Encoding.Default);
                                if (hdr_ent != "")
                                    m_fileENT.WriteLine(hdr_ent);
                            }



                            stLine = "";
                            //exclusion du premier champ numcde
                            for (int i = 1; i < drCompoPropHdr.FieldCount ; i++)
                            {
                                string fldName = drCompoPropHdr.GetName(i);
                                string value = drCompoPropHdr[i].ToString();
                                value = value.Replace("\r", "");
                                value = value.Replace("\n", "##");
                                /*if (fldName == "TEMPSHEURE" || fldName == "TEMPSMINUTE")
                                {
                                    if (value == "0") value = "";
                                    stLine += value + "|";
                                }
                                else*/
                                    stLine += value + separator;
                            }
                            //Suppression du dernier separateur
                            if (stLine.Length >= 1)
                                stLine = stLine.Substring(0, stLine.Length - 1);

                            //string nocdedanem = drCompoPropHdr["cde_code"].ToString();
                            m_fileENT.WriteLine(stLine);
                            Log("Hdr2: " + stLine);
                            bCmdExported = true;

                        } while (drCompoPropHdr.Read());  //desactivation toutes les cmd dans le meme fichier
                        m_fileENT.Close();
                    }
                    drCompoPropHdr.Close();

                    //Export des lignes correspondant aux entetes exportées
                    if (vu_lin != "")      //Pour gestion des export simple sans ligne
                    {
                        foreach (string cde in ListCmdtoFlag)
                        {
                            stQueryLin = @"SELECT * FROM " + vu_lin + " where numcde='" + cde + "' " + order_lin;


                            OdbcCommand m_myCommandPropLin = new OdbcCommand(stQueryLin, odbc_connexion);
                            OdbcDataReader drCompoPropLin = m_myCommandPropLin.ExecuteReader();

                            if (m_fileLIN != null)
                                m_fileLIN.Close();
                            stPathEnt = pathExport + "\\" + prefixe_file_lin + HORODATAGE + "_" + cde + ".tmp";
                            //iCpt++;


                            m_fileLIN = new StreamWriter(stPathEnt, false, System.Text.Encoding.Default);



                            if (drCompoPropLin.Read())
                            {
                                bCmdExported = true;
                                Log("Lin1");
                                int iIdxFile = 1;

                                Log("Open " + stPathEnt);
                                if (hdr_lin != "")
                                    m_fileLIN.WriteLine(hdr_lin);

                                do
                                {


                                    stLine = "";
                                    //exclusion du premier champ numcde
                                    for (int i = 1; i < drCompoPropLin.FieldCount; i++)
                                    {
                                        string fldName = drCompoPropLin.GetName(i);
                                        string value = drCompoPropLin[i].ToString();
                                        value = value.Replace("\r", "");
                                        value = value.Replace("\n", "##");
                                        /*if (fldName == "TEMPSHEURE" || fldName == "TEMPSMINUTE")
                                        {
                                            if (value == "0") value = "";
                                            stLine += value + "|";
                                        }
                                        else*/
                                        stLine += value + separator;
                                    }
                                    //Suppression du dernier separateur
                                    if (stLine.Length >= 1)
                                        stLine = stLine.Substring(0, stLine.Length - 1);


                                    //string nocdedanem = drCompoPropLin["cde_code"].ToString();
                                    m_fileLIN.WriteLine(stLine);
                                    Log("Lin2: " + stLine);
                                    bCmdExported = true;

                                } while (drCompoPropLin.Read());  //desactivation toutes les cmd dans le meme fichier
                                m_fileLIN.Close();
                            }
                            drCompoPropLin.Close();

                        }
                    }


                    Log("Close lin");
                    if (nNbrCde == 0)        //Le noms des colone compte pour une ligne
                    {
                        //suppression des fichiers tmp
                        if (stPathEnt != "")
                            File.Delete(stPathEnt);
                    }
                    else
                    {
                        //Renomage et sauvegarde des fichiers
                        string pathSave = pathExport + "\\" + Now_to_YYYYMMDDhhmmss().Substring(0, 8) + "\\";      //repertoire sauvegarde AAAAMM
                        Directory.CreateDirectory(pathSave);
                        string[] filePaths = Directory.GetFiles(pathExport, "*.tmp");
                        foreach (string fich in filePaths)
                        {
                            //copie de sauvegarde
                            string saveFile = fich.Replace(".tmp", sufixe_file);
                            saveFile = saveFile.Replace(pathExport, pathSave);
                            DeleteFile(pathSave, Path.GetFileName(saveFile));
                            File.Copy(fich, saveFile, true);

                            //Fichier final
                            string newFile = fich.Replace(".tmp", sufixe_file);
                            //PAS De suppression de l'éventuel fichier final existant
                            //A faire si besoin, concatenation des données
                            //Fonction.DeleteFile(textBoxExportMappc.Text, Path.GetFileName(newFile));
                            File.Move(fich, newFile);
                        }

                        //Flagage des commande
                        //Après la création du fichier , mettre un "1" dans Kemsdata pour les type 83 et 84 dans le champs FLAG afin de le pas ré-exporté.
                        bool bFlagageOk = true;  //test commit
                        foreach (string cde in ListCmdtoFlag)   //Ajout pour correction problème flag du 18/5/22
                        {
                            string query = "UPDATE KEMS_DATA SET  flag='5' , date_envoi=getdate() where isnull(flag,'') = ''  and (dat_type="+ type_ent+" or dat_type="+ type_lin + ")";
                            if (type_lin  == "" )
                                query = "UPDATE KEMS_DATA SET  flag='5' , date_envoi=getdate() where isnull(flag,'') = '' and (dat_type=" + type_ent + " )";
                            if (andflag != "")
                                query = " UPDATE KEMS_DATA SET  flag='5' , date_envoi=getdate() where 1=1 " + andflag.Replace("*equal*", "=");
                            if (updateflag != "" )
                            {
                                query = query.Replace("flag='5'", updateflag.Replace("*equal*", "="));
                            }
                            query += " and dat_idx01='" + cde + "'"; //Ajout pour correction problème flag du 18/5/22
                            //ajout param replace_idx01_in_flag_query
                            if (replace_idx01_in_flag_query != "")
                                query = query.Replace("dat_idx01", replace_idx01_in_flag_query);

                            Log("query flag:"+ query);

                            if (GL_bNoFlag == false )
                            {
                                m_myCommandPropUpdate.CommandText = query;
                                if (m_myCommandPropUpdate.ExecuteNonQuery() < 1)
                                {
                                    bFlagageOk = false;
                                    Log("query flag ERROR:" + query);
                                }
                            }
                        }
                        if (bFlagageOk == false)
                        {
                            Log("Erreur sur update");
                        }

                    }
                    System.Threading.Thread.Sleep(1500);
                    //}while (bCmdExported == true ) ;


                    //m_Log.Close();

                    if (m_fileENT != null)
                        m_fileENT.Close();
                    if (m_fileLIN != null)
                        m_fileLIN.Close();
                }

                GC.Collect();

                Log("Fin génération fichier");
                if (keyappalive != "")
                {
                    using (WebClient client = new WebClient())
                    {
                        //Clé d'import appalive
                        //A FAIRE: creation config somgel appalive
                        string s = client.DownloadString("http://sd4.danem.fr/appalive/onthefly.php?idkey="+ keyappalive);
                        Log("BIP appalive: " + s);

                    }
                }

            }            
            catch (SystemException ex)
            {
                Log(ex.Message.ToString());
                //MessageBox.Show(ex.Message.ToString());

            }
            if ( m_Log != null )
                m_Log.Close();

            Close();

        }
        public void Log(string stlog)
        {
            try
            {
                if(m_Log != null)
                {
                    string stLine = DateTime.Now.ToString("dd/MM/yy HH:mm") +";";
                    stLine += stlog;
                    m_Log.WriteLine(stLine);
                    //MessageBox.Show(stLine);
                }
            }            
            catch (SystemException ex)
            {
                Log(ex.Message.ToString());
                //MessageBox.Show(ex.Message.ToString());

            }        
        }
        public string GiveFld(string stLine, int nNum, string pTok)
        {
            char[] delimiter = pTok.ToCharArray();
            string[] split = stLine.Split(delimiter);
            int i = 0;
            foreach (string st in split)
            {
                if (nNum == i)
                    return (st);
                i++;
            }
            return "END";
        }

        private string SetFixedLength(string stLine, int nLength)
        {
            if (stLine.Length<nLength)
            {
                for (int i=stLine.Length;i<nLength;i++)
                {
                    stLine += " ";
                }
            }
            if (stLine.Length > nLength)
                stLine = stLine.Substring(0, nLength);
            return stLine;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            Log("Close Manuel");
            Close();
        }
        static public string Now_to_YYYYMMDDhhmmss()
        {
            string dt = "";
            dt = String.Format("{0}{1:00}{2:00}{3:00}{4:00}{5:00}",
                DateTime.Now.Year,
                DateTime.Now.Month,
                DateTime.Now.Day,
                DateTime.Now.Hour,
                DateTime.Now.Minute,
                DateTime.Now.Second
                );

            return dt;

        }
        static public bool DeleteFile(string path, string file)
        {
            try
            {
                string[] Files = { "" };
                if (file != "")
                    Files = Directory.GetFiles(path, file);
                else
                    Files[0] = path;

                //string stTable=
                bool bfindfile = false;
                foreach (string fich in Files)
                {
                    bfindfile = true;
                    File.Delete(fich);

                }
                return bfindfile;
            }
            catch (Exception ex)
            {
                return false;
            }
            //return true;
        }


        static string controlINI(string valini, string valdefault)
        {
            string ret = valdefault;
            if (valini != null)
                if (valini != "")
                    ret = valini;
            return ret;
        }
        public class IniFile
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="IniFile"/> class.
            /// </summary>
            /// <param name="file">The initialization file path.</param>
            /// <param name="commentDelimiter">The comment delimiter string (default value is ";").
            /// </param>
            public IniFile(string file, string commentDelimiter = ";")
            {
                CommentDelimiter = commentDelimiter;
                TheFile = file;
            }

            /// <summary>
            /// Initializes a new instance of the <see cref="IniFile"/> class.
            /// </summary>
            public IniFile()
            {
                CommentDelimiter = ";";
            }

            /// <summary>
            /// The comment delimiter string (default value is ";").
            /// </summary>
            public string CommentDelimiter { get; set; }

            private string theFile = null;

            /// <summary>
            /// The initialization file path.
            /// </summary>
            public string TheFile
            {
                get
                {
                    return theFile;
                }
                set
                {
                    theFile = null;
                    dictionary.Clear();
                    if (File.Exists(value))
                    {
                        theFile = value;
                        using (StreamReader sr = new StreamReader(theFile))
                        {
                            string line, section = "";
                            while ((line = sr.ReadLine()) != null)
                            {
                                line = line.Trim();
                                if (line.Length == 0) continue;  // empty line
                                if (!String.IsNullOrEmpty(CommentDelimiter) && line.StartsWith(CommentDelimiter))
                                    continue;  // comment

                                if (line.StartsWith("[") && line.Contains("]"))  // [section]
                                {
                                    int index = line.IndexOf(']');
                                    section = line.Substring(1, index - 1).Trim();
                                    continue;
                                }

                                if (line.Contains("="))  // key=value
                                {
                                    int index = line.IndexOf('=');
                                    string key = line.Substring(0, index).Trim();
                                    string val = line.Substring(index + 1).Trim();
                                    string key2 = String.Format("[{0}]{1}", section, key).ToLower();

                                    if (val.StartsWith("\"") && val.EndsWith("\""))  // strip quotes
                                        val = val.Substring(1, val.Length - 2);

                                    if (dictionary.ContainsKey(key2))  // multiple values can share the same key
                                    {
                                        index = 1;
                                        string key3;
                                        while (true)
                                        {
                                            key3 = String.Format("{0}~{1}", key2, ++index);
                                            if (!dictionary.ContainsKey(key3))
                                            {
                                                dictionary.Add(key3, val);
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dictionary.Add(key2, val);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            // "[section]key"   -> "value1"
            // "[section]key~2" -> "value2"
            // "[section]key~3" -> "value3"
            private Dictionary<string, string> dictionary = new Dictionary<string, string>();

            private bool TryGetValue(string section, string key, out string value)
            {
                string key2;
                if (section.StartsWith("["))
                    key2 = String.Format("{0}{1}", section, key);
                else
                    key2 = String.Format("[{0}]{1}", section, key);

                return dictionary.TryGetValue(key2.ToLower(), out value);
            }

            /// <summary>
            /// Gets a string value by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns>The value.</returns>
            /// <seealso cref="GetAllValues"/>
            public string GetValue(string section, string key, string defaultValue = "")
            {
                string value;
                if (!TryGetValue(section, key, out value))
                    return defaultValue;

                return value;
            }

            /// <summary>
            /// Gets a string value by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <returns>The value.</returns>
            /// <seealso cref="GetValue"/>
            public string this[string section, string key]
            {
                get
                {
                    return GetValue(section, key);
                }
            }

            /// <summary>
            /// Gets an integer value by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="minValue">Optional minimum value to be enforced.</param>
            /// <param name="maxValue">Optional maximum value to be enforced.</param>
            /// <returns>The value.</returns>
            public int GetInteger(string section, string key, int defaultValue = 0,
                int minValue = int.MinValue, int maxValue = int.MaxValue)
            {
                string stringValue;
                if (!TryGetValue(section, key, out stringValue))
                    return defaultValue;

                int value;
                if (!int.TryParse(stringValue, out value))
                {
                    double dvalue;
                    if (!double.TryParse(stringValue, out dvalue))
                        return defaultValue;
                    value = (int)dvalue;
                }

                if (value < minValue)
                    value = minValue;
                if (value > maxValue)
                    value = maxValue;
                return value;
            }

            /// <summary>
            /// Gets a double floating-point value by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <param name="minValue">Optional minimum value to be enforced.</param>
            /// <param name="maxValue">Optional maximum value to be enforced.</param>
            /// <returns>The value.</returns>
            public double GetDouble(string section, string key, double defaultValue = 0,
                double minValue = double.MinValue, double maxValue = double.MaxValue)
            {
                string stringValue;
                if (!TryGetValue(section, key, out stringValue))
                    return defaultValue;

                double value;
                if (!double.TryParse(stringValue, out value))
                    return defaultValue;

                if (value < minValue)
                    value = minValue;
                if (value > maxValue)
                    value = maxValue;
                return value;
            }

            /// <summary>
            /// Gets a boolean value by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <param name="defaultValue">The default value.</param>
            /// <returns>The value.</returns>
            public bool GetBoolean(string section, string key, bool defaultValue = false)
            {
                string stringValue;
                if (!TryGetValue(section, key, out stringValue))
                    return defaultValue;

                return (stringValue != "0" && !stringValue.StartsWith("f", true, null));
            }

            /// <summary>
            /// Gets an array of string values by section and key.
            /// </summary>
            /// <param name="section">The section.</param>
            /// <param name="key">The key.</param>
            /// <returns>The array of values, or null if none found.</returns>
            /// <seealso cref="GetValue"/>
            public string[] GetAllValues(string section, string key)
            {
                string key2, key3, value;
                if (section.StartsWith("["))
                    key2 = String.Format("{0}{1}", section, key).ToLower();
                else
                    key2 = String.Format("[{0}]{1}", section, key).ToLower();

                if (!dictionary.TryGetValue(key2, out value))
                    return null;

                List<string> values = new List<string>();
                values.Add(value);
                int index = 1;
                while (true)
                {
                    key3 = String.Format("{0}~{1}", key2, ++index);
                    if (!dictionary.TryGetValue(key3, out value))
                        break;
                    values.Add(value);
                }

                return values.ToArray();
            }
        }
        //Marche mal...
        //recup de cette classe sur https://stackoverflow.com/questions/217902/reading-writing-an-ini-file pour utilisation de fichier ini de parametrage
        /*class IniFile_org   // revision 11
        {
            public string Path;
            string EXE = Assembly.GetExecutingAssembly().GetName().Name;

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern long WritePrivateProfileString(string Section, string Key, string Value, string FilePath);

            [DllImport("kernel32", CharSet = CharSet.Unicode)]
            static extern int GetPrivateProfileString(string Section, string Key, string Default, StringBuilder RetVal, int Size, string FilePath);

            public IniFile_org(string IniPath = null)
            {
                Path = new FileInfo(IniPath ?? EXE + ".ini").FullName;
            }

            public string Read(string Key, string Section = null)
            {
                var RetVal = new StringBuilder(255);
                GetPrivateProfileString(Section ?? EXE, Key, "", RetVal, 255, Path);
                return RetVal.ToString();
            }

            public void Write(string Key, string Value, string Section = null)
            {
                WritePrivateProfileString(Section ?? EXE, Key, Value, Path);
            }

            public void DeleteKey(string Key, string Section = null)
            {
                Write(Key, null, Section ?? EXE);
            }

            public void DeleteSection(string Section = null)
            {
                Write(null, null, Section ?? EXE);
            }

            public bool KeyExists(string Key, string Section = null)
            {
                return Read(Key, Section).Length > 0;
            }
        }*/
    }



}