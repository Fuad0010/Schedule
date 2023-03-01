﻿using Newtonsoft.Json;
using session_repeat_1.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace session_repeat_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string schedulePath = @"C:\Users\DEZHURIK\Desktop\Schedule.txt";
            string schedulePathJson = @"C:\Users\DEZHURIK\Desktop\C_Sharp-Repeat\session_repeat_1\Schedule.json";
            string schedulePathXml = @"C:\Users\DEZHURIK\Desktop\C_Sharp-Repeat\session_repeat_1\Schedule.xml";


            string text = File.ReadAllText(schedulePath);

            string[] lines = text.Replace("\r", "").Split("\n").ToArray();

            List<Session> sessions = new List<Session>();

            Session selectedSession = null;

            for (int i = 0; i < lines.Length; i++)
            {

                string line = lines[i];
                string[] session = line.Split("\"");

                if (session[0]=="")
                {
                    continue;
                }

                if (session[1].Length > 6)
                {
                    string date = session[1];
                    selectedSession = new Session
                    {
                        Date = date,
                        SessionItems = new List<SessionItem>()
                    };
                    sessions.Add(selectedSession);
                }
                else if (session[1].Length < 6)
                {
                    string starttime = session[1];
                    string endtime = session[3];
                    string name = session[5];

                    SessionItem sessionItem = new SessionItem
                    {
                        StartTime = starttime,
                        EndTime = endtime,
                        Name = name
                    };
                    selectedSession.SessionItems.Add(sessionItem);
                }
            }

            string json = JsonConvert.SerializeObject(sessions.ToArray());

            File.WriteAllText(schedulePathJson, json);

            XmlSerializer xsSubmit = new XmlSerializer(typeof(Session[]));
            string xml = "";

            using (var sww = new StringWriter())
            {
                using (XmlWriter writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, sessions.ToArray());
                    xml = sww.ToString();
                }
            }

            File.WriteAllText(schedulePathXml,xml);
        }
    }
}