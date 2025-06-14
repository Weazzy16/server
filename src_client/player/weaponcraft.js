// WEAPON CRAFT //
const callRemote = mp.events.callRemote;
const call = mp.events.call;
const callRemoteUnreliable = mp.events.callRemoteUnreliable;
const browsers = mp.browsers;
const _callRemote = mp._events.callRemote ;
const _call = mp._events.call;
let weaponCraftFracId = 0;

gm.events.add('client.craft.create', (category, activeItemID) => {    
    try
    {
        if(new Date().getTime() - global.lastCheck < 50) return;
        global.lastCheck = new Date().getTime();
        callRemote('server.craft.create', weaponCraftFracId, category, activeItemID);
    }
    catch (e) 
    {
        callRemote("client_trycatch", "player/weaponcraft", "client.craft.create", e.toString());
    }
});

gm.events.add('client.craft.createAmmo', (category, value) => {  
    try
    {
        if(new Date().getTime() - global.lastCheck < 50) return;
        global.lastCheck = new Date().getTime();  
        callRemote('server.craft.createAmmo', weaponCraftFracId, category, value);
    }
    catch (e) 
    {
        callRemote("client_trycatch", "player/weaponcraft", "client.craft.createAmmo", e.toString());
    }
});

gm.events.add('client.craft.close', () => {
    try
    {
        mp.gui.emmit(`window.router.setHud();`);
        global.menuClose();
        weaponCraftFracId = 0;
    }
    catch (e) 
    {
        callRemote("client_trycatch", "player/weaponcraft", "client.craft.close", e.toString());
    }
});

gm.events.add('client.fraction.craft.open', async (fId, weaponJson, ammoJson) => {
    try
    {
        //mp.gui.chat.push(`${frac}:${json}`);
        weaponCraftFracId = fId;
        mp.gui.emmit(
            `window.router.setView("FractionsCraft", ['${weaponJson}','${ammoJson}'])`
        );
        global.menuOpen();
    }
    catch (e) 
    {
        callRemote("client_trycatch", "player/weaponcraft", "client.fraction.craft.open", e.toString());
    }
});