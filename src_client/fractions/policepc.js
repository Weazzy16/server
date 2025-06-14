// POLICE PC //\
const callRemote = mp.events.callRemote;
const call = mp.events.call;
const callRemoteUnreliable = mp.events.callRemoteUnreliable;
const browsers = mp.browsers;
const _callRemote = mp._events.callRemote ;
const _call = mp._events.call;
var policepcOpened = false;
var pcSubmenu;

global.binderFunctions.o_policepc = () => {// U key
	try
	{
		if (!global.loggedin || global.chatActive || global.editing || global.menuCheck() || new Date().getTime() - global.lastCheck < 1000) return;
		callRemote('openCopCarMenu');
		global.lastCheck = new Date().getTime();
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "global.binderFunctions.o_policepc", e.toString());
	}
};

gm.events.add("playerLeaveVehicle", () => 
{
	try
	{
		if(policepcOpened)
		{
			call("closePc");
		}
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "playerLeaveVehicle", e.toString());
	}
})

gm.events.add("openPc", () => {
	try
	{
		if (global.menuCheck()) return;
		policepcOpened = true;
		mp.gui.emmit(`window.router.setView("FractionsPolicecomputer");`);
		gm.discord(translateText("Использует бортовой компьютер"));
		global.menuOpen();
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "openPc", e.toString());
	}
});

gm.events.add("closePc", () => {
	try
	{
		policepcOpened = false;
		mp.gui.emmit(`window.router.setHud();`);
		global.menuClose();
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "closePc", e.toString());
	}
});

global.binderFunctions.c_policepc = () => {
	try
	{
		if(policepcOpened) {
			call("closePc");
		}
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "global.binderFunctions.c_policepc", e.toString());
	}
};

gm.events.add('client:pcMenuExit', () => {
	try
	{
		policepcOpened = false;
		mp.gui.emmit(`window.router.setHud();`);
		global.menuClose();
	}
	catch (e) 
	{
		callRemote("client_trycatch", "fractions/policepc", "client:pcMenuExit", e.toString());
	}
});

gm.events.add('client:pcMenuInput', (callback, data) => {
    callRemote(callback, data);
});

gm.events.add('client:wantedListRequest', () => {
    callRemote('checkWantedList');
});


gm.events.add("executeCarInfo", (model, holder) => {
    mp.gui.emmit(`window.policecomputer.openCar("${model}","${holder}")`);
});

gm.events.add("executePersonInfo", (name, lastname, uuid, fraction_name, pnumber, gender, wantedlvl, lic, houseInfo) => {
    mp.gui.emmit(`window.policecomputer.openPerson("${name}","${lastname}","${uuid}","${fraction_name}","${pnumber}","${gender}","${wantedlvl}","${lic}","${houseInfo}")`);
});

gm.events.add("executeWantedList", (data) => {
    mp.gui.emmit(`window.policecomputer.openWanted('${data}')`);
});