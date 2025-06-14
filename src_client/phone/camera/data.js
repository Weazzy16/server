const callRemote = mp.events.callRemote;
const call = mp.events.call;
const callRemoteUnreliable = mp.events.callRemoteUnreliable;
const browsers = mp.browsers;
const _callRemote = mp._events.callRemote ;
const _call = mp._events.call;
export const faceAnimList = [
    { name: translateText("Обычная"), animName: null },
    { name: translateText("Прицеливание"), animName: "mood_aiming_1" },
    { name: translateText("Злой"), animName: "mood_angry_1" },
    { name: translateText("Пьяный"), animName: "mood_drunk_1" },
    { name: translateText("Весёлый"), animName: "mood_happy_1" },
    { name: translateText("Пострадавший"), animName: "mood_injured_1" },
    { name: translateText("Гнев"), animName: "mood_stressed_1" },
    { name: translateText("Обиженный"), animName: "mood_sulk_1" },
];

export const animationList = [
    { name: translateText("Кулак к груди"), dict: "cellphone@self@franklin@", anim: "chest_bump" },
    { name: translateText("Мир"), dict: "cellphone@self@franklin@", anim: "peace" },
    { name: "West Coast", dict: "cellphone@self@franklin@", anim: "west_coast" },
    { name: translateText("Показать на камеру"), dict: "cellphone@self@michael@", anim: "finger_point" },
    { name: translateText("Поправить лицо"), dict: "cellphone@self@michael@", anim: "run_chin" },
    { name: translateText("Размять шею"), dict: "cellphone@self@michael@", anim: "stretch_neck" },
    { name: translateText("Агрессивный фак"), dict: "cellphone@self@trevor@", anim: "aggressive_finger" },
    { name: translateText("Гордый фак"), dict: "cellphone@self@trevor@", anim: "proud_finger" },
    { name: translateText("Перерезать глотку"), dict: "cellphone@self@trevor@", anim: "throat_slit" },
    { name: translateText("Воздушный поцелуй"), dict: "anim@mp_player_intselfieblow_kiss" },
    { name: translateText("ОК"), dict: "anim@mp_player_intselfiedock" },
    { name: translateText("Ура"), dict: "anim@mp_player_intselfiejazz_hands" },
    { name: translateText("Фак"), dict: "anim@mp_player_intselfiethe_bird" },
    { name: translateText("Большой палец"), dict: "anim@mp_player_intselfiethumbs_up" },
    { name: translateText("Дергать рукой"), dict: "anim@mp_player_intselfiewank" },
];

export const timeCycle = [
    "",
    "NG_filmic01",
    "NG_filmic02",
    "NG_filmic03",
    "NG_filmic04",
    "NG_filmic05",
    "NG_filmic06",
    "NG_filmic07",
    "NG_filmic08",
    "NG_filmic09",
    "NG_filmic10",
    "NG_filmic11",
    "NG_filmic12",
    "NG_filmic13",
    "NG_filmic14",
    "NG_filmic15",
    "NG_filmic16",
    "NG_filmic17",
    "NG_filmic18",
    "NG_filmic19",
    "NG_filmic20",
    "NG_filmic21",
    "NG_filmic22",
    "NG_filmic23",
    "NG_filmic24",
    "NG_filmic25",
];

export const clampMin = (value, minClamp, maxClamp) => Math.min(maxClamp, Math.max(minClamp, value));
