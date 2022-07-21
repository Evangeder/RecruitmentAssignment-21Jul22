using System.Xml.Serialization;

namespace RecruitmentAssignmentSente.XSD
{
    [XmlType(AnonymousType = true)]
    [XmlRoot("uczestnik", Namespace = "", IsNullable = true)]
    public partial class Employee
    {
        private Employee[] employeeField;
        private int idField;

        [XmlElement("uczestnik")]
        public Employee[] Employees
        {
            get => employeeField;
            set => employeeField = value;
        }

        [XmlAttribute()]
        public int id
        {
            get => idField;
            set => idField = value;
        }
    }

    [XmlType(AnonymousType = true)]
    [XmlRoot("struktura", Namespace = "", IsNullable = true)]
    public partial class Structure
    {
        private Employee[] employeeField;

        [XmlElement("uczestnik")]
        public Employee[] Employees
        {
            get => employeeField;
            set => employeeField = value;
        }
    }
}