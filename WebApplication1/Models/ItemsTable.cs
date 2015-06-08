using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class ItemsTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }

        public String Summary { get; set; }

        public int Price { get; set; }

        public String Description { get; set; }

        public String PicturePath { get; set; }

        public ItemsTable()
            : base("")
        {
        }

        public ItemsTable(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "ITEMS";
        }
        public override void GetValues()
        {
            ID = long.Parse(FieldsValues[0]);
            Summary = FieldsValues[1];
            Price = int.Parse(FieldsValues[2]);
            Description = FieldsValues[3];
            PicturePath = FieldsValues[4];
        }

        public override void Insert()
        {
            InsertRecord(Summary, Price, Description, PicturePath);
        }

        public override void Update()
        {
            UpdateRecord(ID, Summary, Price, Description, PicturePath);
        }
    }
}
