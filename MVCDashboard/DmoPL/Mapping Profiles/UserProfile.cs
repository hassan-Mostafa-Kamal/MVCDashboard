using AutoMapper;
using DemoDAL.Models;
using DmoPL.Models;

namespace DmoPL.Mapping_Profiles
{
	public class UserProfile:Profile
	{
		public UserProfile()
		{
			CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
			
		}
	}
}
