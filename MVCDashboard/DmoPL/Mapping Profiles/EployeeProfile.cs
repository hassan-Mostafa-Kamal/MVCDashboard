using AutoMapper;
using DemoDAL.Models;
using DmoPL.Models;

namespace DmoPL.Mapping_Profiles
{
	public class EployeeProfile:Profile 
	{
		public EployeeProfile()
		{
			CreateMap<EmployeeViewModel, Empployee>().ReverseMap();
		}
	}
}
