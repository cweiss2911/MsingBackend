﻿using Confluent.Kafka;
using Confluent.Kafka.Serialization;
using FileReader.Notifier;
using Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;

namespace FileReader.FileReadHandler
{
    public class ReadHandler : IReadHandler
    {
        private INotifier _notifier;
        private const string Topic = "FileRead";

        public ReadHandler(INotifier notifier)
        {
            _notifier = notifier;
        }

        public void HandleReadFile(FileInfo fileInfo)
        {
            SendMessage(fileInfo.Name);
            DeleteFile(fileInfo.FullName);
        }

        private void DeleteFile(string fullName)
        {
            Console.WriteLine("Deleting");
            File.Delete(fullName);
            Console.WriteLine("Deleted");
        }


        private void SendMessage(string payload)
        {
            Messages.Message message = new Messages.Message()
            {
                Type = "success",
                Payload = payload,
            };

            _notifier.Notify(message);
        }

    }
}
