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
			
			if(splits.length < 2) {
				return;
			}
			
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
					var subsections = section.split("/");
					data.extraParams = subsections;
					eval("me.renderer.render" + subsections[0] + "(data);");
				});
			}
		}
	}
	
	
	
	this.getKeys = function getKeys(obj) {
		var keys = [];
		for(var key in obj){
			keys.push(key);
		}
		return keys;
	}
		
	
	
	this.process = function process() {
		this.renderer = new AltairStudios.Core.Admin.Renderer();
		this.renderer.configure();
		setInterval("coreProcess.checkAnchor()", 300);
		$('.dropdown-toggle').dropdown();
		$(".nav > li", "#navbar").click(function() {
			$(".nav > li", "#navbar").removeClass("active");
			$(this).addClass("active");
		});
	}
}

$(document).ready(function() {
	coreProcess = new AltairStudios.Core.Admin.Process();
	coreProcess.process();
});