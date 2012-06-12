AltairStudios.Core.Admin.Process = function() {
	var checkAnchorTime = 300;
	var currentAnchor = null;
	var renderer = null;
	var currentSection = "";
	var currentSubsection = "";
	
	
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
			var tempSection = section.split("/");
			var process = null;
			var i = 0;
			
			if(tempSection[0] == "web") {
				process = "web";
				section = tempSection[1];
			}
			
			sectionSplit = section.split("-");
			section = "";
			
			for(i = 0; i < sectionSplit.length; i++) {
				section += sectionSplit[i].substring(0,1).toUpperCase() + sectionSplit[i].substring(1);
			}
			
			me.currentSection = section;
			
			if(section == "Logout") {
				document.location = path + "/Admin/Logout";
			} else if(process == "web") {
				var subsections = section.split("/");
				me.currentSubsection = subsections;
				
				$.get(path + "/Admin/" + subsections[0], function(html) {
					me.renderer.renderHtml(html);
				});
			} else {
				$.getJSON(path + "/Admin/" + section, function(data) {
					var subsections = section.split("/");
					data.extraParams = subsections;
					me.currentSubsection = subsections;
					eval("me.renderer.render" + subsections[0] + "(data);");
				});
			}
		}
	}
	
	
	
	this.editDatabaseViewer = function editDatabaseViewer(element, keys) {
		var me = this;
		
		$("td.dataviewer-content", element).each(function() {
			me.renderer.renderDatabaseViewerInput($(this));
		});
		
		var button = $(".coreProcess-editDatabaseViewer", element);
		button.html("Guardar");
		button.off("click");
		button.on("click", function() {
			me.currentAnchor = null;
			var json = {};
			
			$("td.dataviewer-content input", element).each(function() {
				eval("json." + $(this).attr("name") + "='" + $(this).attr("value") + "'");
			});
	        
	        $.post(path + "/Admin/DatabaseUpdateData/" + me.currentSubsection[1], json, function(data) {
	        	alert(me.currentSubsection);
	        	alert(data.Result);
	        }, "json");
			return false;
		});

		return false;
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