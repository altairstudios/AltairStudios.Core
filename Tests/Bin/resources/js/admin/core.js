AltairStudios.Core.Admin.Process = function() {
	var checkAnchorTime = 300;
	var currentAnchor = null;
	var renderer = null;
	
	
	this.checkAnchor = function checkAnchor() {
		var me = this;
		if(currentAnchor != document.location.hash) {
			currentAnchor = document.location.hash;
			
			if (!currentAnchor) {
				currentAnchor = "#!/home";
				document.location = path + "/Admin/Desktop" + currentAnchor;
			}
			
			var splits = currentAnchor.split("#!/");
			var section = splits[1];
			var i = 0;
			
			sectionSplit = section.split("-");
			section = "";
			
			for(i = 0; i < sectionSplit.length; i++) {
				section += sectionSplit[i].substring(0,1).toUpperCase() + sectionSplit[i].substring(1);
			}
			
			if(section == "Logout") {
				document.location = path + "/Admin/Logout";
			} else {
				$.getJSON(path + "/Admin/" + section, function(data) {
					//data = $.parseJSON(data);
					eval("me.renderer.render" + section + "(data);");
				});
			}
		}
	}
		
	
	
	this.process = function process() {
		this.renderer = new AltairStudios.Core.Admin.Renderer();
		this.renderer.configure();
		setInterval("coreProcess.checkAnchor()", 300);
	}
}

$(document).ready(function() {
	coreProcess = new AltairStudios.Core.Admin.Process();
	coreProcess.process();
});