Ext.define('AdminDesktop.App', {
    extend: 'Ext.ux.desktop.App',
    //appFolder: 'app',

    requires: [
        'Ext.window.MessageBox',

        'Ext.ux.desktop.ShortcutModel',

        'AdminDesktop.SystemStatus',
        'AdminDesktop.VideoWindow',
        'AdminDesktop.GridWindow',
        'AdminDesktop.TabWindow',
        'AdminDesktop.AccordionWindow',
        'AdminDesktop.Notepad',
        'AdminDesktop.BogusMenuModule',
        'AdminDesktop.BogusModule',

//        'AdminDesktop.Blockalanche',
        'AdminDesktop.Settings'
    ],

    init: function() {
        // custom logic before getXYZ methods get called...
		
		this.callParent();
        
        // now ready...
		Ext.Ajax.request({
			url: 'Admin/GetUser',
			success: function (response) {
				json = Ext.decode( response.responseText );
				
				var menu = adminDesktop.getDesktop().taskbar.startMenu;
				if (menu.rendered) {
	                menu.setTitle(json.Name + " " + json.Surname);
	            } else {
	                menu.title = json.Name + " " + json.Surname;
	            }
			}
		});
    },

    getModules : function(){
        return [
            new AdminDesktop.VideoWindow(),
            //new AdminDesktop.Blockalanche(),
            new AdminDesktop.SystemStatus(),
            new AdminDesktop.GridWindow(),
            new AdminDesktop.TabWindow(),
            new AdminDesktop.AccordionWindow(),
            new AdminDesktop.Notepad(),
            new AdminDesktop.BogusMenuModule(),
            new AdminDesktop.BogusModule()
        ];
    },

    getDesktopConfig: function () {
        var me = this, ret = me.callParent();

        return Ext.apply(ret, {
            //cls: 'ux-desktop-black',

            contextMenuItems: [
                { text: 'Change Settings', handler: me.onSettings, scope: me }
            ],

            shortcuts: Ext.create('Ext.data.Store', {
                model: 'Ext.ux.desktop.ShortcutModel',
                data: [
                   /* { name: 'Grid Window', iconCls: 'grid-shortcut', module: 'grid-win' },
                    { name: 'Accordion Window', iconCls: 'accordion-shortcut', module: 'acc-win' },
                    { name: 'Notepad', iconCls: 'notepad-shortcut', module: 'notepad' },
                    { name: 'System Status', iconCls: 'cpu-shortcut', module: 'systemstatus'}*/
                ]
            }),

            wallpaper: 'Bin/wallpapers/Blue-Sencha.jpg',
            wallpaperStretch: false
        });
    },

    // config for the start menu
    getStartConfig : function() {
        var me = this, ret = me.callParent();

        return Ext.apply(ret, {
            title: 'Name',
            iconCls: 'user',
            height: 300,
            toolConfig: {
                width: 150,
                items: [
                    {
                        text:'Configuración',
                        iconCls:'settings',
                        handler: me.onSettings,
                        scope: me
                    },
                    '-',
                    {
                        text:'Salir',
                        iconCls:'logout',
                        handler: me.onLogout,
                        scope: me
                    }
                ]
            }
        });
    },

    getTaskbarConfig: function () {
        var ret = this.callParent();

        return Ext.apply(ret, {
            quickStart: [
                //{ name: 'Accordion Window', iconCls: 'accordion', module: 'acc-win' },
                //{ name: 'Grid Window', iconCls: 'icon-grid', module: 'grid-win' }
            ],
            trayItems: [
                { xtype: 'trayclock', flex: 1 }
            ]
        });
    },

    onLogout: function () {
        Ext.Msg.confirm('Salir', '¿Está seguro que desea salir de la aplicación?');
    },

    onSettings: function () {
        var dlg = new AdminDesktop.Settings({
            desktop: this.desktop
        });
        dlg.show();
    }
});
