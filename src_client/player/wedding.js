const callRemote = mp.events.callRemote;
const call = mp.events.call;
const callRemoteUnreliable = mp.events.callRemoteUnreliable;
const browsers = mp.browsers;
const _callRemote = mp._events.callRemote ;
const _call = mp._events.call;
let weddingToggled = false;

gm.events.add('client.wedding.married', (name, typeSurname) => {
    callRemote("server.wedding.married", name, typeSurname);
    global.binderFunctions.closeWedding ();
});

gm.events.add('client.wedding.open', (type, price) => {
    if (global.menuCheck()) return;
    global.menuOpen();
    weddingToggled = true;
    mp.gui.emmit(`window.router.setView("PlayerWedding", {type: ${type}, price: ${price}});`);
    gm.discord(translateText("Общается со священником"));
});

global.binderFunctions.closeWedding = () => {
    if (!weddingToggled) 
        return;

    mp.gui.emmit(`window.router.setHud();`);
    weddingToggled = false;
    global.menuClose();
}