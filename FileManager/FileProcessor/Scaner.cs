﻿using FileManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileProcessor;

// TODO: Methods refactoring!!!
public class Scaner
{
    private RegexAnalizer regexAnalizer;
    public string[] DirMasks { get; set; } = { };
    public string[] FileMasks { get; set; } = { };
    public FSObjectContainer Container { get; set; } = new FSObjectContainer();

    public Scaner()
    {
        regexAnalizer = new RegexAnalizer();
    }


    public void FindDirectories(string rootPath, List<DirectoryInfo> target)
    {
        DirectoryInfo dir = new DirectoryInfo(rootPath);

        DirectoryInfo[] dirs = dir.GetDirectories();

        List<DirectoryInfo> findedDirs =  regexAnalizer.Filter<DirectoryInfo>(dirs, DirMasks);

        target.AddRange(findedDirs);

        foreach(DirectoryInfo d in dirs.Except<DirectoryInfo>(findedDirs))
            FindDirectories(d.FullName,target);
    }

    public void FindFiles(DirectoryInfo dir, bool findInSubDirectory, List<FileInfo> target)
    {
        if (DirectoryIsAcces.CheckAccess(dir))
        {

            DirectoryInfo[] dirs = dir.GetDirectories();
            FileInfo[] files = dir.GetFiles();
            target.AddRange(regexAnalizer.Filter<FileInfo>(files, FileMasks));
            if (findInSubDirectory)
            {
                foreach (DirectoryInfo d in dirs)
                    FindFiles(d, findInSubDirectory, target);
            }
        }
    }

    public void FindAll(string rootPath)
    {
        DirectoryInfo dir = new DirectoryInfo(rootPath);

        DirectoryInfo[] dirs = dir.GetDirectories();
        FileInfo[] files = dir.GetFiles();

        Container.Files.AddRange(regexAnalizer.Filter<FileInfo>(files, FileMasks));

        List<DirectoryInfo> findedDirs = regexAnalizer.Filter<DirectoryInfo>(dirs, DirMasks);

        Container.Dirs.AddRange(findedDirs);

        foreach (DirectoryInfo d in dirs.Except<DirectoryInfo>(findedDirs))
            FindAll(d.FullName);
    }
}
