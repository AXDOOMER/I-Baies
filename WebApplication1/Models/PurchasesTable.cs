using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace WebApplication1.Models
{
    public class PurchaseTable : SqlExpressUtilities.SqlExpressWrapper
    {
        public long ID { get; set; }

        public long ItemID { get; set; }

        public DateTime Date { get; set; }

        public int Quantity { get; set; }

        public long UserID { get; set; }

        public PurchaseTable()
            : base("")
        {
        }

        public PurchaseTable(Object connexionString)
            : base(connexionString)
        {
            SQLTableName = "PURCHASES";
        }
        public override void GetValues()
        {
            ID = long.Parse(FieldsValues[0]);
            ItemID = long.Parse(FieldsValues[1]);
            Date = DateTime.Parse(FieldsValues[2]);
            Quantity = int.Parse(FieldsValues[3]);
            UserID = long.Parse(FieldsValues[4]);
        }

        public override void Insert()
        {
            InsertRecord(ItemID, Date, Quantity, UserID);
        }

        public override void Update()
        {
            UpdateRecord(ID, ItemID, Date, Quantity, UserID);
        }
    }
}
