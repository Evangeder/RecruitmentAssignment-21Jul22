using System.Collections.Generic;

namespace RecruitmentAssignmentSente.DAO
{
    class Employee
    {
        public int Id { get; init; }
        public Employee Boss { get; init; }
        public List<Employee> Subordinates { get; init; }
        public int Level { get; init; }

        public int MonetaryCommision = 0;
    }
}
