using AutoMapper;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Dtos;

namespace Business.Mappings
{
    public class Mappings:Profile
    {
        public Mappings()
        {
            CreateMap<About, AboutViewModel>();
            CreateMap<AboutViewModel, About>();
            CreateMap<Blog, CreateUpdateBlogModel>();
            CreateMap<CreateUpdateBlogModel, Blog>();
            CreateMap<Slider, SliderViewModel>();
            CreateMap<SliderViewModel, Slider>();
            CreateMap<Ministration, MinistrationViewModel>();
            CreateMap<MinistrationViewModel, Ministration>();
            CreateMap<SiteInformation, SiteInformationViewModel>();
            CreateMap<SiteInformationViewModel, SiteInformation>();
            CreateMap<User, UserPhotoViewModel>();
            CreateMap<UserPhotoViewModel, User>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<CategoryViewModel, Category>();
            CreateMap<Contact, ContactViewModel>();
            CreateMap<ContactViewModel, Contact>();
            CreateMap<User, RegisterViewModel>();
            CreateMap<RegisterViewModel, User>();
            CreateMap<User, UserViewModel>();
            CreateMap<UserViewModel, User>();
            CreateMap<User, UpdateUserModel>();
            CreateMap<UpdateUserModel, User>();
            CreateMap<User, UserWithPhotoViewModel>();
            CreateMap<UserWithPhotoViewModel, User>();
            CreateMap<Role, AddRoleModel>();
            CreateMap<AddRoleModel, Role>();
            CreateMap<Role, UpdateRoleModel>();
            CreateMap<UpdateRoleModel, Role>();
            CreateMap<Role, RoleViewModel>();
            CreateMap<RoleViewModel, Role>();
            CreateMap<Tag, TagCreateUpdateModel>();
            CreateMap<TagCreateUpdateModel, Tag>();
            CreateMap<Comment, CommentViewModel>();
            CreateMap<CommentViewModel, Comment>();
            CreateMap<Comment, UpdateCommentByAdmin>();
            CreateMap<UpdateCommentByAdmin, Comment>();
            CreateMap<Reply, ReplyViewModel>();
            CreateMap<ReplyViewModel, Reply>();
            CreateMap<Reply, UpdateReplyByAdmin>();
            CreateMap<UpdateReplyByAdmin, Reply>();
        }
    }
}
