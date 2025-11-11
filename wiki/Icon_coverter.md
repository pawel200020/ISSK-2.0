#Icon converter
tutorial based on step by step instruction

The default Blazor navigation menu is in NavMenu.razor. Its CSS is in NavMenu.razor.css, and contains such exciting classes as bi-plus-square-fill-nav-menu and bi-house-door-fill-nav-menu.

These are all [Bootstrap Icons](https://icons.getbootstrap.com/icons). Here I’ll show you how to add another Bootstrap icon to the NavMenu.

I’m going to add the “Envelope exclamation fill” icon. From the Bootstrap page, copy the SVG code to your clipboard, e.g.

example icon xml
```xml
<svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-envelope-exclamation-fill" viewBox="0 0 16 16">
    <path d="M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414zM0 4.697v7.104l5.803-3.558zM6.761 8.83l-6.57 4.026A2 2 0 0 0 2 14h6.256A4.5 4.5 0 0 1 8 12.5a4.49 4.49 0 0 1 1.606-3.446l-.367-.225L8 9.586zM16 4.697v4.974A4.5 4.5 0 0 0 12.5 8a4.5 4.5 0 0 0-1.965.45l-.338-.207z"/>
    <path d="M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1.5a.5.5 0 0 1-1 0V11a.5.5 0 0 1 1 0m0 3a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0"/>
</svg>
```
We’re gonna need to URL encode the above. Paste the above into Notepad, and then Ctrl-H to replace:
1. currentColor with white
1. " with '
1. < with %3C
1. \> with %3E
1. and remove all new lines

which should give you
``` 
%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='white' class='bi bi-envelope-exclamation-fill' viewBox='0 0 16 16'%3E%3Cpath d='M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414zM0 4.697v7.104l5.803-3.558zM6.761 8.83l-6.57 4.026A2 2 0 0 0 2 14h6.256A4.5 4.5 0 0 1 8 12.5a4.49 4.49 0 0 1 1.606-3.446l-.367-.225L8 9.586zM16 4.697v4.974A4.5 4.5 0 0 0 12.5 8a4.5 4.5 0 0 0-1.965.45l-.338-.207z'/%3E%3Cpath d='M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1.5a.5.5 0 0 1-1 0V11a.5.5 0 0 1 1 0m0 3a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0'/%3E%3C/svg%3E
```
Now open the NavMenu.razor.css and copy one of the existing styles such as .bi-list-nested-nav-menu and give it a new name, such as .bi-envelope-fill-nav-menu. Paste in the above after “data:image/svg+xml,”.
```css
.bi-envelope-fill-nav-menu {
    background-image: url("data:image/svg+xml,%3Csvg xmlns='http://www.w3.org/2000/svg' width='16' height='16' fill='white' class='bi bi-envelope-exclamation-fill' viewBox='0 0 16 16'%3E%3Cpath d='M.05 3.555A2 2 0 0 1 2 2h12a2 2 0 0 1 1.95 1.555L8 8.414zM0 4.697v7.104l5.803-3.558zM6.761 8.83l-6.57 4.026A2 2 0 0 0 2 14h6.256A4.5 4.5 0 0 1 8 12.5a4.49 4.49 0 0 1 1.606-3.446l-.367-.225L8 9.586zM16 4.697v4.974A4.5 4.5 0 0 0 12.5 8a4.5 4.5 0 0 0-1.965.45l-.338-.207z'/%3E%3Cpath d='M12.5 16a3.5 3.5 0 1 0 0-7 3.5 3.5 0 0 0 0 7m.5-5v1.5a.5.5 0 0 1-1 0V11a.5.5 0 0 1 1 0m0 3a.5.5 0 1 1-1 0 .5.5 0 0 1 1 0'/%3E%3C/svg%3E");
}
```
Now you can use the bi-envelope-fill-nav-menu class in the NavMenu.razor:
```xml
<div class="nav-item px-3">
    <NavLink class="nav-link" href="rating-unit-dead-letter">
        <span class="bi bi-envelope-fill-nav-menu" aria-hidden="true"></span> Rating Unit Dead Letter
    </NavLink>
</div>
```

based on [MATT WORKS BLOG](https://mattfrear.com/2024/02/27/customize-blazors-navmenu/)
