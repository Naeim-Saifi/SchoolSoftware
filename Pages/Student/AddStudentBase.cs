using AdminDashboard.Server.Models.Student;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Generic;

namespace AdminDashboard.Server.Pages.Student
{
    public class AddStudentBase : ComponentBase
    {

        public AddStudentViewModel AddStudentViewModel = new AddStudentViewModel();
        public class Gender
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<Gender> genderDetails = new List<Gender>
        {
            new Gender(){Id=1,Value="Male"},
            new Gender(){Id=2,Value="Female"},
        };


        public class CastCategory
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        public List<CastCategory> casteCategory = new List<CastCategory>
        {
            new CastCategory(){Id=1,Value="General"},
            new CastCategory(){Id=2,Value="OBC"},
        };

        public class Religion
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }
        public List<Religion> religion = new List<Religion>
        {
            new Religion(){Id=1,Value="Hindu"},
            new Religion(){Id=2,Value="Christian"},
            new Religion(){Id=3,Value="Muslim"},
            new Religion(){Id=4,Value="Jain"},
            new Religion(){Id=5,Value="Sikh"},
            new Religion(){Id=6,Value="Not Avilable"},
        };

        public class StudentType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<StudentType> studentType = new List<StudentType>
        {
            new StudentType(){Id=1,Value="Day Bording"},
            new StudentType(){Id=2,Value="Bording"},
            new StudentType(){Id=3,Value="Day Scholar"},
        };

        public class AdmissionType
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<AdmissionType> admissionType = new List<AdmissionType>
        {
            new AdmissionType(){Id=1,Value="New Admission"},
            new AdmissionType(){Id=2,Value="Old Admission"},
            new AdmissionType(){Id=2,Value="Other"},
        };

        public class House
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<House> house = new List<House>
        {
            new House(){Id=1,Value="Day Bording"},
            new House(){Id=2,Value="Bording"},
            new House(){Id=3,Value="Day Scholar"},
        };

        public class ClassName
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<ClassName> className = new List<ClassName>
        {
            new ClassName(){Id=1,Value="NUR"},
            new ClassName(){Id=2,Value="LKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="1"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
            new ClassName(){Id=3,Value="UKG"},
        };

        public class SectionName
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<SectionName> sectionName = new List<SectionName>
        {
            new SectionName(){Id=1,Value="Day Bording"},
            new SectionName(){Id=2,Value="Bording"},
            new SectionName(){Id=3,Value="Day Scholar"},
        };

        public class FeeCategory
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<FeeCategory> feeCategory = new List<FeeCategory>
        {
            new FeeCategory(){Id=1,Value=""},
            new FeeCategory(){Id=2,Value=""},
            new FeeCategory(){Id=3,Value=""},
        };

        public class TransportMode
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<TransportMode> transportMode = new List<TransportMode>
        {
            new TransportMode(){Id=1,Value="OWN"},
            new TransportMode(){Id=2,Value="School"},
            
        };

        public class BusStop
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<BusStop> busStop = new List<BusStop>
        {
            new BusStop(){Id=1,Value="Day Bording"},
            new BusStop(){Id=2,Value="Bording"},
            new BusStop(){Id=3,Value="Day Scholar"},
        };
        public class BloodGroup
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<BloodGroup> bloodGroup = new List<BloodGroup>
        {
            new BloodGroup(){Id=1,Value="A+"},
            new BloodGroup(){Id=2,Value="B+"},
            new BloodGroup(){Id=3,Value="AB+"},
            new BloodGroup(){Id=3,Value="O+"},
            new BloodGroup(){Id=3,Value="A-"},
            new BloodGroup(){Id=3,Value="B-"},
            new BloodGroup(){Id=3,Value="AB-"},
            new BloodGroup(){Id=3,Value="O-"},
            new BloodGroup(){Id=3,Value="Not Avila"},
        };

        public class State
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<State> state = new List<State>
        {
            new State(){Id=1,Value="Haryana"},
            new State(){Id=2,Value="Telanga"},
            new State(){Id=3,Value="India"},
        };

        public class City
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<City> city = new List<City>
        {
            new City(){Id=1,Value="city-1"},
            new City(){Id=2,Value="city-2"},
            new City(){Id=3,Value="other"},
        };

        public class Classs
        {
            public int Id { get; set; }
            public string Value { get; set; }
        }

        public List<Classs> classs = new List<Classs>
        {
             new Classs(){Id=1,Value="NUR"},
            new Classs(){Id=2,Value="LKG"},
            new Classs(){Id=3,Value="UKG"},
            new Classs(){Id=3,Value="UKG"},
            new Classs(){Id=3,Value="UKG"},
            new Classs(){Id=3,Value="UKG"},
        };



            public class Section
            {
            public int Id { get; set; }
            public string Value { get; set; }
            }

        public List<Section> sections = new List<Section>
        {
            new Section(){Id=1,Value=""},
            new Section(){Id=2,Value=""},
            new Section(){Id=3,Value=""},
        };

        public async void OnValidSubmit(EditContext contex)
        {
        }
    };

    
    
}
