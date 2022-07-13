﻿using System;
using FluentFTP;

namespace Client
{
    public static class Modify
    {
        public static int Delete(ref FtpClient client, Commands.Delete file)
        {
            return 0;
        }

        /// <summary>
        /// Changes the permissions of a specified remote file.
        /// </summary>
        /// <param name="client">Ftp server endpoint.</param>
        /// <param name="file">Permission levels and file path</param>
        /// <returns>0 if successful else -1.</returns>
        /// <exception cref="Exception">Library methods could fail.</exception>
        public static int Permissions(ref FtpClient client, Commands.Permissions file)
        {
            
            int result = -1;
            try
            {
                // Invoke library method to execute permission change.
                client.SetFilePermissions(file.FilePath, ((file.Owner * 100) + (file.Group * 10) + file.Others));
                result = 0;
            }
            catch (Exception exc)
            {
                throw new Exception($"[Client.Modify.Permissions(client)] Failed to Change Permissions of {file.FilePath}. Auto Exception Message: {exc.Message}");
            }
            return result;
        }

        /// <summary>
        /// Will rename a file on local or remote directory depending on where it exists.
        /// </summary>
        /// <param name="client">ftpClient on which potential file to rename exists.</param>
        /// <returns>0 if the file was renamed successfully, -1 if it failed.</returns>
        /// <exception cref="Exception">Could be library call critical errors or System.IO.Directory failure.</exception>
        public static int Rename(FtpClient client, Commands.Rename file)
        {
            int result = -1;
            try
            {
                // Check if the file exists on the remote server.
                if (client.FileExists(file.Name))
                {
                    // rename remote file
                    result = client.MoveFile(file.Name, file.Remote) ? 0 : -1;
                }
                // check if local file exists with given name
                else if (Directory.Exists(file.Name))
                {
                    Directory.Move(file.Name, file.Local);
                    result = 0;
                }
            }
            catch(Exception exc)
            {
                throw new Exception($"[Client.Modify.Rename(client, file, oNm, nNm)] Failed to rename file {file.Name}. Auto Exception Msg: {exc.Message}");
            }

            return result;
        }    
    }
}
