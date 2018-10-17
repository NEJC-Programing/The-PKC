﻿using Newtonsoft.Json;
using TPKC.API;

namespace TPKC.API
{
    /// <summary>
    /// The Commands for packed
    /// </summary>
    enum Commands
    {
        Read,
        Write,
        Check
    }
    /// <summary>
    /// The Packed Data Type
    /// </summary>
    struct Packed
    {

        public Commands CMD { get; set; }
        public double VER { get; set; }
        public DBEntry[] Data { get; set; }
        public DBEntry Data_s { get; set; }
        public int[] DOCS { get; set; }
        public int DOCS_s { get; set; }
        public bool[] CheckResponce { get; }
        public bool CheckResponce_s { get; }

        /// <summary>
        /// Loads Packed from json that came from server
        /// </summary>
        /// <param name="JSON">JSON value of a Packed</param>
        public Packed(string JSON)
        {
            this = JsonConvert.DeserializeObject<Packed>(JSON);
        }

        /// <summary>
        /// Gets the JSON Data of This Packed
        /// </summary>
        /// <returns>JOSN Value of Packed</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(Data);
        }
    }
}

namespace TPKC
{
    /// <summary>
    /// The DBEntry Doc Type
    /// </summary>
    class DBEntry
    {
        public DBEntry(string DocTitle, string DocBody, string DocAuthor, int DocID = 0)
        {
            
            ID = DocID;
            Title = DocTitle;
            Body = DocBody;
            Author = DocAuthor;
            if (ID < 1)
                ID = GetNextID();
        }
        public DBEntry(string JSON)
        {
            var x = JsonConvert.DeserializeObject<DBEntry>(JSON);
            ID = x.ID;
            Author = x.Author;
            Body = x.Body;
            Title = x.Title;

            if (ID < 1)
                ID = GetNextID();
        }
        private struct IDResponce
        {
            public int id;
            public int ver;

        }
        private int GetNextID()
        {
            return JsonConvert.DeserializeObject<IDResponce>(new System.Net.WebClient().DownloadString("http://localhost/nextid/")).id;
        }

        private int e_id = 0;

        public int ID {
            get {
                if (e_id > 0)
                    return e_id;
                else
                {
                    e_id = GetNextID();
                    return e_id;
                }
            }
            private set
            {
                if (value > 0)
                    e_id = value;
                else
                    e_id = GetNextID();
            }
        } // the id

        public string Title { get; set; } // text
        public string Body { get; set; } // html
        public string Author { get; set; } // text

        public bool Changed
        {
            get
            {
                Packed packed = new Packed
                {
                    CMD = Commands.Check,
                    VER = 0,
                    Data_s = this,
                    DOCS_s = ID
                };
                return !new Packed(new Server("http://localhost/").SendJSON(packed.ToString())).CheckResponce_s;
            }
        } // cheks agents server if the docs are the same

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        /// <summary>
        /// Checks if This Object is Equal to obj
        /// </summary>
        /// <param name="obj">the object to check</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }
            return ((DBEntry)obj).ID == ID;
        }
        public override int GetHashCode()
        {
            return ID;
        }
    }
}
