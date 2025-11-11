# Adding new node to app
1. Create razor page with routing
2. Create nav link in NavMenu.razor

	```
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="weather">
                <span class="bi bi-gear-fill-nav-menu" aria-hidden="true"></span> Node
            </NavLink>
        </div>```
3. Add new icon
   1. find desired icon in [bootstrap icon base](https://icons.getbootstrap.com/icons)
   2. convert icon into css style
      1. using dedicated [online converter](https://codepen.io/elliz/full/ygvgay)
      2. using [icon converter](Icon_coverter.md)
4. Decide which roles should see this tab
5. Add content
6. Enjoy!