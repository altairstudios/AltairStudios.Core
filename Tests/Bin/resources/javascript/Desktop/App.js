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
                    { name: 'Grid Window', iconCls: 'grid-shortcut', module: 'grid-win' },
                    { name: 'Accordion Window', iconCls: 'accordion-shortcut', module: 'acc-win' },
                    { name: 'Notepad', iconCls: 'notepad-shortcut', module: 'notepad' },
                    { name: 'System Status', iconCls: 'cpu-shortcut', module: 'systemstatus'}
                ]
            }),

            wallpaper: 'Content/wallpapers/Blue-Sencha.jpg',
            wallpaperStretch: false
        });
    },

    // config for the start menu
    getStartConfig : function() {
        var me = this, ret = me.callParent();

        return Ext.apply(ret, {
            title: 'Don Griffin 2',
            iconCls: 'user',
            height: 300,
            toolConfig: {
                width: 100,
                items: [
                    {
                        text:'Settings',
                        iconCls:'settings',
                        handler: me.onSettings,
                        scope: me
                    },
                    '-',
                    {
                        text:'Logout',
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
                { name: 'Accordion Window', iconCls: 'accordion', module: 'acc-win' },
                { name: 'Grid Window', iconCls: 'icon-grid', module: 'grid-win' }
            ],
            trayItems: [
                { xtype: 'trayclock', flex: 1 }
            ]
        });
    },

    onLogout: function () {
        Ext.Msg.confirm('Logout', 'Are you sure you want to logout?');
    },

    onSettings: function () {
        var dlg = new AdminDesktop.Settings({
            desktop: this.desktop
        });
        dlg.show();
    }
});

