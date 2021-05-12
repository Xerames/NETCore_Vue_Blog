const routes = [
  {
    path: "/",
    component: () => import("layouts/MainLayout.vue"),
    children: [
      { path: "", name: "Home", component: () => import("pages/Home.vue") },
      { path: "/about", name: "About", component: () => import("pages/About.vue")},
      { path: "/blog", name: "Blog", component: () => import("pages/Blog.vue") },
      { path: "/blog/:slug", name: "blog-detail", component: () => import("pages/BlogDetail.vue") },
      { path: "/category/:categoryname", name: "category-blogs", component: () => import("pages/Blog.vue") },
      { path: "/tag/:tagname", name: "tag-blogs", component: () => import("pages/Blog.vue") },
      { path:"/login",name:"Login",component:()=>import("pages/account/Login.vue")},
      { path:"/register",name:"Register",component:()=>import("pages/account/Register.vue")},
      { path:"/forgetpassword",name:"ForgotPassword",component:()=>import("pages/account/ForgetPassword.vue")},
      { path:"/profile",name:"Profile",component:()=>import("pages/account/Profile.vue"), meta: { requiresLogin: true }},
      { path:"/changepassword",name:"ChangePassword",component:()=>import("pages/account/ChangePassword.vue"), meta: { requiresLogin: true }},
      { path:"/changephoto",name:"ChangePhoto",component:()=>import("pages/account/ChangePhoto.vue"), meta: { requiresLogin: true }},
      { path:"/confirmemail/userId/:userId/token/:token",name:"ConfirmEmail",component:()=>import("pages/account/ConfirmEmail.vue")},
      { path:"/resetpassword/email/:email/token/:token",name:"ResetPassword",component:()=>import("pages/account/ResetPassword.vue")},
      { path:"/accessdenied",name:"AccessDenied",component:()=>import("pages/account/AccessDenied.vue")},
    ],
  },
  {
    path: "/dashboard",
    component: () => import("layouts/AdminLayout.vue"),
    children:[
      { path: "/dashboard/contacts", name: "DashboardContacts", component: () => import("pages/dashboard/Contacts.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/ministrations", name: "DashboardMinistrations", component: () => import("pages/dashboard/Ministrations.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/abouts", name: "DashboardAbouts", component: () => import("pages/dashboard/Abouts.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/categories", name: "DashboardCategories", component: () => import("pages/dashboard/Categories.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/comments", name: "DashboardComments", component: () => import("pages/dashboard/Comments.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/replies", name: "DashboardReplies", component: () => import("pages/dashboard/Replies.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/sliders", name: "DashboardSliders", component: () => import("pages/dashboard/Sliders.vue") , meta: { requiresAuth: true } },
      { path: "/dashboard/siteinformation", name: "DashboardSiteInformations", component: () => import("pages/dashboard/SiteInformation.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/tags", name: "DashboardTags", component: () => import("pages/dashboard/Tags.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/blogs", name: "DashboardBlogs", component: () => import("pages/dashboard/Blogs.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/updateblog/:id", name: "DashboardUpdateBlog", component: () => import("pages/dashboard/UpdateBlog.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/addblog", name: "DashboardAddBlog", component: () => import("pages/dashboard/AddBlog.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/users", name: "DashboardUsers", component: () => import("pages/dashboard/Users.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/roles", name: "DashboardRoles", component: () => import("pages/dashboard/Roles.vue") , meta: { requiresAuth: true }},
      { path: "/dashboard/logs", name: "DashboardLogs", component: () => import("pages/dashboard/Logs.vue"), meta: { requiresAuth: true } },
      { path: "/dashboard/updatetag/:id", name: "DashboardUpdateTag", component: () => import("pages/dashboard/UpdateTag.vue") , meta: { requiresAuth: true }},
    ]
  }
];

export default routes;
