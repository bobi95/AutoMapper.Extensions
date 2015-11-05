using AutoMapper;
using AutoMapper.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class User
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }

    [MapToType(typeof(User))]
    class EditUserVM
    {
        public string FirstName { get; set; }

        [IgnoreMember("LastName")]
        public string LastName { get; set; }

        [MapToMember("Email")]
        public string Details { get; set; }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Assembly.GetExecutingAssembly().AutomapNamespace("Tests");

            var userVm = new EditUserVM() { FirstName = "Borislav", LastName = "Rangelov", Details="bobi9502@yahoo.com" };
            var user = new User();
            Mapper.Map(userVm, user);

            Console.WriteLine(user.FirstName);
            Console.ReadKey(true);
        }
    }
}
