using AutoMapper;
using DemoDAL.Models;
using DmoPL.Models;

namespace DmoPL.Mapping_Profiles
{
	public class DepartmentProfile:Profile
	{
		public DepartmentProfile()
		{
			CreateMap<DepartmentViewModel,Department>().ReverseMap(); 
		}
	}
}
