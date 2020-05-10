﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace commonLib
{
    public class fileUtl
    {
        public static string pb(string path, string dirOrFile)
        {
            string ret;
            ret = Path.Combine(path, dirOrFile);
            return ret;
        }
        public static string ensureDir(string path, string dir)
        {
            string ret = "";
            if (!Directory.Exists(path))
                ret = "path not existed";
            else
            {
                string newDir = pb(path, dir);
                if (!Directory.Exists(newDir))
                    Directory.CreateDirectory(newDir);
            }
            return ret;
        }
        public static string FileInfo2Name(FileInfo fi)
        {
            string ret = fi.Name;
            return ret;
        }
        public static string newestFile(string path, string ext="")
        {
            string ret = "";
            var directory = new DirectoryInfo(path);// "C:\\MyDirectory");
            var myFile = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).First();
            ret = FileInfo2Name(myFile);
            //// or...
            //var myFile = directory.GetFiles()
            //             .OrderByDescending(f => f.LastWriteTime)
            //             .First();
            return ret;
        }
        public static string purgePath(string path, bool
            purgeAll=false, bool realDelete=false)
        {
            string ret = "";
            string newest = newestFile(path);
            var directory = new DirectoryInfo(path);
            var myFiles = (from f in directory.GetFiles()
                          orderby f.LastWriteTime descending
                          select f).ToList();
            foreach (FileInfo afile in myFiles)
            {
                string filename = FileInfo2Name(afile);
                if (!purgeAll && filename == newest)
                    continue;
                File.Move(afile.FullName, afile.FullName + ".del");
                if (realDelete)
                    File.Delete(afile.FullName + ".del");
            }
            return ret;
        }
        public static string genTimeStamp()
        {
            string ret = "";
            ret = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return ret;
        }
        public static string write1string(string path, string astr)
        {
            string ret = "";
            string newfile = genTimeStamp() + ".txt";
            purgePath(path, true);
            StreamWriter sw = new StreamWriter(pb( path, newfile));
            sw.WriteLine(astr);
            sw.Close();
            sw = null;
            return ret;
        }
        public static string read1string(string path, out string
            readStr)
        {
            string ret = "";
            readStr = "";
            string newest = newestFile(path);
            StreamReader sr = new StreamReader(pb(path, newest));
            readStr = sr.ReadLine();
            sr.Close();
            sr = null;
            return ret;
        }
        public static string json2file(string fileFullpath
            , string json)
        {
            string ret = "";
            File.AppendAllText(fileFullpath, json);
            return ret;
        }
        public static string file2json(string fileFullpath
            , out string json)
        {
            string ret = "";
            json = File.ReadAllText(fileFullpath);
            return ret;
        }
    }
}