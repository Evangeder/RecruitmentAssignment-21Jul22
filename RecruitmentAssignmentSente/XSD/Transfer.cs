using System.Xml.Serialization;

namespace XSD
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("przelew", Namespace = "", IsNullable = true)]
    public partial class Transfer
    {
        private int fromField;
        private int amountField;

        [XmlAttribute("od")]
        public int From
        {
            get => fromField;
            set => fromField = value;
        }

        [XmlAttribute("kwota")]
        public int Amount
        {
            get => amountField;
            set => amountField = value;
        }
    }

    [XmlType(AnonymousType = true)]
    [XmlRoot("przelewy", Namespace = "", IsNullable = true)]
    public partial class Transfers
    {
        private Transfer[] transferField;

        [XmlElement("przelew")]
        public Transfer[] Transfer
        {
            get => transferField;
            set => transferField = value;
        }
    }
}