﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Features;
using Volo.Abp.GlobalFeatures;
using Volo.Abp.UI.Navigation;
using Volo.CmsKit.Features;
using Volo.CmsKit.GlobalFeatures;
using Volo.CmsKit.Localization;
using Volo.CmsKit.Permissions;

namespace Volo.CmsKit.Admin.Web.Menus;

public class CmsKitAdminMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private async Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        await AddCmsMenuAsync(context);
    }

    private async Task AddCmsMenuAsync(MenuConfigurationContext context)
    {
        var l = context.GetLocalizer<CmsKitResource>();

        var cmsMenus = new List<ApplicationMenuItem>();

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.Pages.PagesMenu,
                l["Pages"].Value,
                "/Cms/Pages",
                "fa fa-file-alt",
                order: 6)
            .RequireFeatures(CmsKitFeatures.PageEnable)
            .RequireGlobalFeatures(typeof(PagesFeature))
            .RequirePermissions(CmsKitAdminPermissions.Pages.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.Blogs.BlogsMenu,
                l["Blogs"],
                "/Cms/Blogs",
                "fa fa-blog",
                order: 1)
            .RequireFeatures(CmsKitFeatures.BlogEnable)
            .RequireGlobalFeatures(typeof(BlogsFeature))
            .RequirePermissions(CmsKitAdminPermissions.Blogs.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.BlogPosts.BlogPostsMenu,
                l["BlogPosts"],
                "/Cms/BlogPosts",
                "fa fa-file-signature",
                order: 2)
            .RequireFeatures(CmsKitFeatures.BlogEnable)
            .RequireGlobalFeatures(typeof(BlogsFeature))
            .RequirePermissions(CmsKitAdminPermissions.BlogPosts.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.Tags.TagsMenu,
                l["Tags"].Value,
                "/Cms/Tags",
                "fa fa-tags",
                order: 7)
            .RequireFeatures(CmsKitFeatures.TagEnable)
            .RequireGlobalFeatures(typeof(TagsFeature))
            .RequirePermissions(CmsKitAdminPermissions.Tags.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.Comments.CommentsMenu,
                l["Comments"].Value,
                "/Cms/Comments",
                "fa fa-comments",
                order: 3)
            .RequireFeatures(CmsKitFeatures.CommentEnable)
            .RequireGlobalFeatures(typeof(CommentsFeature))
            .RequirePermissions(CmsKitAdminPermissions.Comments.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.Menus.MenusMenu,
                l["Menus"],
                "/Cms/Menus/Items",
                "fa fa-stream",
                order: 5)
            .RequireFeatures(CmsKitFeatures.MenuEnable)
            .RequireGlobalFeatures(typeof(MenuFeature))
            .RequirePermissions(CmsKitAdminPermissions.Menus.Default));

        cmsMenus.Add(new ApplicationMenuItem(
                CmsKitAdminMenus.GlobalResources.GlobalResourcesMenu,
                l["GlobalResources"],
                "/Cms/GlobalResources",
                "bi bi-code-slash",
                order: 4)
            .RequireFeatures(CmsKitFeatures.GlobalResourceEnable)
            .RequireGlobalFeatures(typeof(GlobalResourcesFeature))
            .RequirePermissions(CmsKitAdminPermissions.GlobalResources.Default));

        if (cmsMenus.Any())
        {
            var cmsMenu = context.Menu.FindMenuItem(CmsKitAdminMenus.GroupName);

            if (cmsMenu == null)
            {
                cmsMenu = new ApplicationMenuItem(
                    CmsKitAdminMenus.GroupName,
                    l["Cms"],
                    icon: "far fa-newspaper");

                context.Menu.AddItem(cmsMenu);
            }

            foreach (var menu in cmsMenus)
            {
                cmsMenu.AddItem(menu);
            }
            
        }
    }
}
