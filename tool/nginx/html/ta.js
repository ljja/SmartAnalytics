/*
This file is part of ta.js 0.1

author:  mr.wangya@qq.com

example:

<script type="text/javascript">
    var _wyma = _wyma || [];
    _wyma.push(['_setAccount', '400-8018-521']);
    _wyma.push(['_trackPageview']);
    _wyma.push(['_trackSubmit']);
    _wyma.push(['_trackClick']);
    _wyma.push(['_trackMover']);
    _wyma.push(['_trackTag']);
	_wyma.push(['_trackClose']);
	
    (function() {
        var ma = document.createElement('script');
		ma.type = 'text/javascript';
		ma.async = true;
		ma.src = ('https:' == document.location.protocol ? 'https://a' : 'http://a') + '.tongji.com/ta.js';
        var s = document.getElementsByTagName('script')[0];
		s.parentNode.insertBefore(ma, s);
    })();
</script>

*/

(function () {
	
	//初始化追踪对象
	var params = {};
	var exts   = ''; 
	var mbuffer= [];
	
	//初始化QueryString
	getQueryString(true);
	
	//初始化domain、url、referrer属性
	if(document) {
		params.domain = document.domain || '';
		params.url = document.URL || '';
		params.referrer = $_GET['referrer'] || document.referrer || '';
	}
	
	//初始化sh、sw、cd属性
	if(window && window.screen) {
		params.sh = window.screen.height || 0;
		params.sw = window.screen.width || 0;
		params.cd = window.screen.colorDepth || 0;
	}
	
	//初始化lang属性
	if(navigator) {
		params.lang = navigator.language || '';
	}
	
	//初始化追踪对象
	if(_wyma) {
		
		//遍历初始化命令
		for(var i in _wyma) {
			
			//按命令初始化操作
			switch(_wyma[i][0]) {
				
				//初始化追踪账户标识
				case '_setAccount':
					params.account = _wyma[i][1];
					break;
				
				//初始化网页打开事件
				case '_trackPageview':
					dopageview();
					break;
				
				//初始化文本框元素事件
				case '_trackSubmit':
					var list = document.getElementsByTagName("INPUT");
					for (var i=0; i<list.length; i++) {
						if ( list[i].type.toUpperCase() == 'SUBMIT' || list[i].type.toUpperCase() == 'BUTTON') {
							if (list[i].addEventListener) {
								list[i].addEventListener("mousedown", docollect, false);
							} else {
								list[i].attachEvent("onmousedown", docollect);
							}
						}
					}
					break;

				//初始化鼠标单击事件
				case '_trackClick':
					if (document.addEventListener) {
						document.addEventListener("mousedown", doclick, false);
					} else {
						document.attachEvent("onmousedown", IE8doclick);
					}
					break;
				
				//初始化鼠标移动轨迹数据(包含鼠标相对顶部的偏移量)
				case '_trackMouseroll':
					if (document.addEventListener) {
						document.addEventListener("DOMMouseScroll", doMouseRoll, false);
					} else {
						document.attachEvent("onmousewheel", doMouseRoll);
					}
					break;
				
				//初始化鼠标轨迹事件
				case '_trackMover':
					if (document.addEventListener) {
						document.addEventListener("mouseover", domover, false);
					} else {
						document.attachEvent("onmouseover", IE8domover);
					}
					break;
					
				//初始化鼠标单击clstag属性元素事件
				case '_trackTag':
					var _allElement = document.getElementsByTagName("*");
					for(var i=0; i<_allElement.length; i++){
						if(_allElement[i] && _allElement[i].getAttribute('clstag')){
							if (_allElement[i].addEventListener) {
								_allElement[i].addEventListener("mousedown", doclstag, false);
							} else {
								_allElement[i].attachEvent("onmousedown", doclstag);
							}
						}
					}
					break;
				
				//追踪网页关闭事件
				case '_trackClose':
					if (window.addEventListener) {
						window.addEventListener("beforeunload",doclose, false);
					} else {
						window.attachEvent("onbeforeunload", doclose);
					}
					break;
				
				default:
					break;
			}
		}
	}
	
	//追踪网页打开事件
	function dopageview(){
		params.event = '_trackPageview';
		params.exts = '';
		
		var wyma_guestuid = '';
		var wyma_reguid = '';
		
		var wyma_guestuidelm = document.getElementById('wyma_guestuid');
		if(wyma_guestuidelm != undefined && wyma_guestuidelm.value != ''){
			wyma_guestuid = wyma_guestuidelm.value;
		}
		
		var wyma_reguidelm = document.getElementById('wyma_reguid');
		if(wyma_reguidelm != undefined && wyma_reguidelm.value != ''){
			wyma_reguid = wyma_reguidelm.value;
		}
		
		params.exts = wyma_guestuid + '|' + wyma_reguid;
		
		dosubmit();
	}
	
	//追踪网页关闭事件
	function doclose(event){
		params.event = '_trackClose';
		params.exts = '';
		dosubmit();
	}

	//追踪clstag自定义属性单击事件数据
	function doclstag(data){
		params.event = '_trackTag';
		params.exts = '';
		params.exts = this.getAttribute('clstag');
		dosubmit();
	}

	//追踪鼠标点击事件数据IE>=9
	function doclick(event) {
		event = event? event: window.event;
		params.exts = event.layerX + '|' + event.layerY + '|';
		params.event = '_trackClick';

		if(event.target) {
			if(event.target.tagName.toUpperCase()=="A"){
	 			params.exts += event.target.getAttribute("href");
	 		}
		} else {
			if(event.srcElement.tagName.toUpperCase()=="A"){
				params.exts += event.srcElement.getAttribute("href");
			}
		}

		dosubmit();
	}
	
	//追踪鼠标点击事件数据IE<=8
	function IE8doclick(event) {
		event = event? event: window.event;
		params.exts = event.x + '|' + event.y + '|';
		params.event = '_trackClick';

		if(event.target) {
			if(event.target.tagName.toUpperCase()=="A"){
	 			params.exts += event.target.getAttribute("href");
	 		}
		} else {
			if(event.srcElement.tagName.toUpperCase()=="A"){
				params.exts += event.srcElement.getAttribute("href");
			}
		}

		dosubmit();
	}

	//追踪鼠标移动轨迹数据IE>=9
	function domover(event) {
		event = event ? event: window.event;
		mbuffer.push(event.layerX + ',' + event.layerY);
		
		if( mbuffer.length >= 20 ){
			params.event = '_trackMover';
			params.exts = mbuffer.join('|');
			mbuffer = [];
			dosubmit();
		}
	}
	
	//追踪鼠标移动轨迹数据IE<=8
	function IE8domover(event) {
		event = event ? event: window.event;
		mbuffer.push(event.x + ',' + event.y);
		
		if( mbuffer.length >= 20 ){
			params.event = '_trackMover';
			params.exts = mbuffer.join('|');
			mbuffer = [];
			dosubmit();
		}
	}

	//追踪鼠标移动轨迹数据(包含鼠标相对顶部的偏移量)
	function doMouseRoll(event) {
		event = event || window.event; 
		var scrollX = document.documentElement.scrollLeft || document.body.scrollLeft; 
		var scrollY = document.documentElement.scrollTop || document.body.scrollTop; 
		var x = event.pageX || event.clientX + scrollX; 
		var y = event.pageY || event.clientY + scrollY;
		mbuffer.push(x + ',' + y);
		
		if( mbuffer.length >= 20 ){
			params.event = '_trackMouseroll';
			params.exts = mbuffer.join('|');
			mbuffer = [];
			dosubmit();
		}
	}

	//追踪input元素文本框数据
	function docollect() {
		params.exts = '';
		params.event = '_trackSubmit';
		
		var list = document.getElementsByTagName("INPUT");
		
		for (var i=0; i<list.length; i++) {
		        if ((list[i].type.toUpperCase() == 'TEXT' || list[i].type.toUpperCase() == 'HIDDEN') && list[i].value.length <= 32){
				params.exts += list[i].value + '|';
			}
		}
		
		dosubmit();
	}	

	//动态生成一个图片请求
	function dosubmit() {
		var args = '';
		
		for(var i in params) {
			if(args != '') {
				args += '&';
			}
			args += i + '=' + encodeURIComponent(params[i]);
		}
		
		args += '&_random=' + Math.random();
		
		var img = new Image(1, 1);
		img.src = ('https:' == document.location.protocol ? 'https://a' : 'http://a') + '.tongji.com/1.gif?' + args;
	}

	//初始化QueryString
	function getQueryString(isAddToGlobal, globalName){
		var paramObject = {};
		var queryString = window.location.search.substr(1); 
		var params = queryString.split("&");
		for (var i=0,param; param=params[i++];) {
			param = param.split('=');
			param[0] = decodeURIComponent(param[0]);
			param[1] = decodeURIComponent(param[1]);
			paramObject[ param[0] ] = param[1];
		}
		if(isAddToGlobal){
			globalName = globalName || '$_GET';
			window[globalName] = paramObject;
		}
		return paramObject;
	}

})();
