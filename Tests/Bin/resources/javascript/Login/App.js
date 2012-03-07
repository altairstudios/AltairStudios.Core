Ext.define('AdminDesktop.Login.App', {
    extend: 'Ext.window.Window',
    
    title : 'Acceso',
	closable : true,
	closeAction : 'hide',
	    //animateTarget: this,
	width : 600,
	    /*height: 350,*/
	layout : 'border',
	bodyStyle : 'padding: 5px;',


	items: [{ region: 'center', items: [Ext.create('Ext.form.Panel', {
		    //renderTo: Ext.getBody(),
		    title: 'Introduzca sus datos de acceso',
		    bodyStyle: 'padding:5px 5px 0',
		    width: '100%',
		    fieldDefaults: {
		        labelAlign: 'top',
		        msgTarget: 'side'
		    },
		    defaults: {
		        border: false,
		        xtype: 'panel',
		        flex: 1,
		        layout: 'anchor'
		    },
		    layout: 'hbox',
		    items: [{
		        items: [{
		            xtype:'textfield',
		            fieldLabel: 'Usuario',
		            anchor: '100%',
		            name: 'first',
					//vtype:'text',
					id: 'user'
		        }, {
		            xtype:'textfield',
		            fieldLabel: 'Contraseña',
		            anchor: '100%',
		            name: 'password',
		            //vtype:'password',
		            id: 'password'
		        }]
		    }],
		    buttons: ['->', {
		        text: 'Acceder',
		        handler: function() {
					Ext.Ajax.request({
					   url: 'Authorize',
					   success: function (response) {
							json = Ext.decode( response.responseText );
							
							if(json.error != null) {
								return;
							} else {
								document.location = "../Admin";
							}
						},
					   failure: function () { console.log('failure');},
					   headers: {
					       'my-header': 'foo'
					   },
					   params: { Email: Ext.getCmp('user').getValue(), Password: Ext.getCmp('password').getValue() }
					});
		        }
		    }]
		})]
	}]
		
});