﻿using System.Threading.Tasks;
using System.Windows;
using FileHost.Models;
using Newtonsoft.Json;

namespace FileHost.FilesManagement
{
    public class FolderCreator
    {
        private DataAccess DataAccess { get; } = new DataAccess();

        public async Task<FolderItem> CreateFolder(string folderName)
        {
            var folderItem = new FolderItem
            {
                Name = folderName,
            };

            var docResult = await DataAccess.PostAsJson(string.Empty, folderItem);

            if (!docResult.IsSuccessStatusCode)
            {
                MessageBox.Show($"Error: Some error occurred.\nFolder: {folderItem.Name}");
                return null;
            }

            var doc = JsonConvert.DeserializeAnonymousType(await docResult.Content.ReadAsStringAsync(), new { Id = string.Empty, Rev = string.Empty });
            folderItem.Id = doc.Id;
            folderItem.Revision = doc.Rev;

            return folderItem;
        }
    }
}
        