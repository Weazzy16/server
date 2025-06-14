<script>
    import { TimeFormat } from 'api/moment';
    import { serverDateTime } from 'store/server';
    import Header from './header.svelte';
    import { currentPage, currentWeather } from '../stores';

    // Подписки на хранилища, которые вы уже используете
    import { charOrganizationID, charFractionID, charIsLeader, charBankMoney, charData, charWorkID } from 'store/chars';
    import fraction from 'json/fraction.js';
    import { otherStatsData } from 'store/account';

    import MarketplaceIcon from "../../phonenew/assets/images/marketplace.svg";

    export let visible = false;
    let selectCharData = $charData;

    let useVisible = -1;
    $: {
        if (useVisible != visible) {
            if (visible && $otherStatsData.Name) {
                selectCharData = $otherStatsData;
            } else if (visible && !$otherStatsData.Name && selectCharData !== $charData) {
                selectCharData = $charData;
            } else if (!visible && $otherStatsData.Name) {
                selectCharData = $charData;
                window.accountStore.otherStatsData ('{}');
            }
            useVisible = visible;
        }
    }

    export let onSelectedView;

    // userData для фракции/организации
    let userData = {
        targetOrganizationID: 0,
        changeOrganizationID: 0,
        timerIdOrganizationID: 0,
        OrganizationID: 0,
        targetFractionID: 0,
        changeFractionID: 0,
        timerIdFractionID: 0,
        FractionID: 0,
        targetLeader: 0,
        changeLeader: 0,
        timerIdLeader: 0,
        Leader: 0,
    };

    import { onMount } from 'svelte';
    onMount(async () => {
        // Следим за изменениями OrganizationID/FractionID/Leader
        charOrganizationID.subscribe(value => {
            if (userData.OrganizationID !== value) {
                CounterUpdate("OrganizationID", value);
            }
        });
        charFractionID.subscribe(value => {
            if (userData.FractionID !== value) {
                CounterUpdate("FractionID", value);
            }
        });
        charIsLeader.subscribe(value => {
            if (userData.Leader !== value) {
                CounterUpdate("Leader", value);
            }
        });
    });

    // Пример функции, которая "анимирует" смену значений (CountUp)
    const CounterUpdate = (args, value) => {
        if (userData["timerId" + args])
            clearTimeout(userData["timerId" + args]);

        userData["change" + args] = userData[args] > value 
            ? (0 - (userData[args] - value)) 
            : (value - userData[args]);

        userData[args] = value;

        userData["timerId" + args] = setTimeout(() => {
            userData["timerId" + args] = 0;
            userData["change" + args] = 0;
            if (!userData["target" + args]) {
                userData["target" + args] = new CountUp("target" + args, value);
            } else {
                userData["target" + args].update(value);
            }
        }, !userData["target" + args] ? 0 : 5000);
    }

    // Иконки приложений (пример)
    import MapsIcon from '../assets/images/maps.png';
    import GalleryIcon from '../assets/images/gallery.png';
    import TaxiIcon from '../assets/images/taxi.png';
    import CarsIcon from '../assets/images/rent.png';
    import NewsIcon from '../assets/images/news.png';
    import MechIcon from '../assets/images/mech.png';
    import PropertyIcon from '../assets/images/Home.svg';
    import TinderIcon from '../assets/images/tinder.png';
    import RadioIcon from '../assets/images/radio.png';
    import ForbesIcon from '../assets/images/forbes.png';
    import TruckerIcon from '../assets/images/trucker.png';
    import AucIcon from '../assets/images/auction.png';
    import CallIcon from '../assets/images/call.png';
    import MessagesIcon from '../assets/images/messages.png';
    import CameraIcon from '../assets/images/camera.png';
    import SettingsIcon from '../assets/images/settings.png';
    import GiftIcon from '../assets/images/gift.png';

    let menuArray = [
        {
            name: "GPS",
            icon: MapsIcon,
            link: "maps"
        },
        {
            name: "Galerie",
            icon: GalleryIcon,
            link: "gallery"
        },
        {
            name: "Eigentum",
            icon: PropertyIcon,
            link: "property"
        },
        {
            name: "Fahrzeug",
            icon: CarsIcon,
            link: "cars"
        },
        {
            name: "W.News",
            icon: NewsIcon,
            link: "news"
        },
        {
            name: "Trucker",
            icon: TruckerIcon,
            link: "trucker",
            jobId: 6
        },
        {
            name: "Taxi",
            icon: TaxiIcon,
            link: "taxi"
        },
        {
            name: "Mechaniker",
            icon: MechIcon,
            link: "mech"
        },
        {
            name: "Auktion",
            icon: AucIcon,
            link: "auction"
        },
        {
            name: "Geschenk",
            icon: GiftIcon,
            link: 101
        },
        {
            name: "Tinder",
            icon: TinderIcon,
            link: "tinder"
        },
    ];

    import { fade } from 'svelte/transition';
    import { onMessage } from "@/views/player/hudevo/ipad/data";
    import { executeClientAsyncToGroup, executeClientToGroup, executeClient } from "api/rage";

    // Функция перехода на страницу/приложение
    const onSelectPage = (pageName) => {
        if (typeof pageName === "string") {
            if (pageName === "marketPlace") {
                executeClient("client.ipad.close");
                executeClient("client.marketPlace.openApp");
            }

            if (pageName === "camera") {
                executeClientAsyncToGroup("getGallery").then((result) => {
                    result = JSON.parse(result);
                    if (result.length >= 25) {
                        currentPage.set("gallery");
                        window.notificationAdd(4, 9, `Speicherplatz voll! Bitte löschen Sie Ihre alte Fotos`, 3000);
                    } else {
                        currentPage.set("camera");
                    }
                });
            } else {
                currentPage.set(pageName);
            }
        } else if (typeof pageName === "number") {
            onMessage(pageName);
            executeClientToGroup("messageDefault", pageName);
        }
        onSelectedView(pageName);
    }

    // Загрузка фонового изображения
    let background = "";
    executeClientAsyncToGroup("settings.wallpaper").then((result) => {
        background = result;
    });

    // --- Фракция ---
    function handleFractionClick() {
        if (userData.FractionID > 0) {
            onSelectedView("Fractions");
        } else {
            window.notificationAdd(4, 9, "У вас нет фракции!", 3000);
        }
    }

    // --- Организация ---
    function handleOrganizationClick() {
        if (userData.OrganizationID > 0) {
            onSelectedView("Organization");
        } else {
            window.notificationAdd(4, 9, "У вас нет организации!", 3000);
        }
    }

    // --- Дом ---
    function handleHouseClick() {
        if (selectCharData.houseId) {
            onSelectPage("home");
        } else {
            window.notificationAdd(4, 9, "У вас нет дома!", 3000);
        }
    }

    // --- Бизнес ---
    function handleBizClick() {
        if (selectCharData.BizId) {
            onSelectPage("biz");
        } else {
            window.notificationAdd(4, 9, "У вас нет бизнеса!", 3000);
        }
    }
</script>

<!-- Верхняя панель (часы, дата, и т.д.) -->
<div class="headbar">
    <div class="leftbar">
        <p>{TimeFormat($serverDateTime, "H:mm,   dddd,   DD MMMM")}</p>
    </div>
    <div class="rightbar">
        <svg width="17" height="10" viewBox="0 0 17 10" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M13.5761 0.617317C13.5 0.801088 13.5 1.03406 13.5 1.5V8.5C13.5 8.96594 13.5 9.19891 13.5761 9.38268C13.6776 9.62771 13.8723 9.82239 14.1173 9.92388C14.3011 10 14.5341 10 15 10C15.4659 10 15.6989 10 15.8827 9.92388C16.1277 9.82239 16.3224 9.62771 16.4239 9.38268C16.5 9.19891 16.5 8.96594 16.5 8.5V1.5C16.5 1.03406 16.5 0.801088 16.4239 0.617317C16.3224 0.372288 16.1277 0.177614 15.8827 0.0761205C15.6989 0 15.4659 0 15 0C14.5341 0 14.3011 0 14.1173 0.0761205C13.8723 0.177614 13.6776 0.372288 13.5761 0.617317Z" fill="white"/>
            <path d="M9 3.5C9 3.03406 9 2.80109 9.07612 2.61732C9.17761 2.37229 9.37229 2.17761 9.61732 2.07612C9.80109 2 10.0341 2 10.5 2C10.9659 2 11.1989 2 11.3827 2.07612C11.6277 2.17761 11.8224 2.37229 11.9239 2.61732C12 2.80109 12 3.03406 12 3.5V8.5C12 8.96594 12 9.19891 11.9239 9.38268C11.8224 9.62771 11.6277 9.82239 11.3827 9.92388C11.1989 10 10.9659 10 10.5 10C10.0341 10 9.80109 10 9.61732 9.92388C9.37229 9.82239 9.17761 9.62771 9.07612 9.38268C9 9.19891 9 8.96594 9 8.5V3.5Z" fill="white"/>
            <path d="M4.57612 4.61732C4.5 4.80109 4.5 5.03406 4.5 5.5V8.5C4.5 8.96594 4.5 9.19891 4.57612 9.38268C4.67761 9.62771 4.87229 9.82239 5.11732 9.92388C5.30109 10 5.53406 10 6 10C6.46594 10 6.69891 10 6.88268 9.92388C7.12771 9.82239 7.32239 9.62771 7.42388 9.38268C7.5 9.19891 7.5 8.96594 7.5 8.5V5.5C7.5 5.03406 7.5 4.80109 7.42388 4.61732C7.32239 4.37229 7.12771 4.17761 6.88268 4.07612C6.69891 4 6.46594 4 6 4C5.53406 4 5.30109 4 5.11732 4.07612C4.87229 4.17761 4.67761 4.37229 4.57612 4.61732Z" fill="white"/>
            <path d="M0.0761205 6.11732C0 6.30109 0 6.53406 0 7V8.5C0 8.96594 0 9.19891 0.0761205 9.38268C0.177614 9.62771 0.372288 9.82239 0.617317 9.92388C0.801088 10 1.03406 10 1.5 10C1.96594 10 2.19891 10 2.38268 9.92388C2.62771 9.82239 2.82239 9.62771 2.92388 9.38268C3 9.19891 3 8.96594 3 8.5V7C3 6.53406 3 6.30109 2.92388 6.11732C2.82239 5.87229 2.62771 5.67761 2.38268 5.57612C2.19891 5.5 1.96594 5.5 1.5 5.5C1.03406 5.5 0.801088 5.5 0.617317 5.57612C0.372288 5.67761 0.177614 5.87229 0.0761205 6.11732Z" fill="white"/>
        </svg>                        
        <svg width="15" height="10" viewBox="0 0 15 10" fill="none" xmlns="http://www.w3.org/2000/svg">
            <path d="M13.0047 4.246C13.1182 4.3525 13.2932 4.353 13.4037 4.243L14.4677 3.179C14.5822 3.064 14.5827 2.8745 14.4647 2.763C12.6557 1.0505 10.2142 0 7.52669 0C4.83919 0 2.39769 1.0505 0.58869 2.763C0.47069 2.8745 0.47119 3.064 0.58569 3.179L1.64969 4.243C1.76019 4.353 1.93519 4.3525 2.04869 4.246C3.48319 2.9015 5.41019 2.077 7.52669 2.077C9.64319 2.077 11.5702 2.9015 13.0047 4.246Z" fill="white"/>
            <path d="M10.5578 6.69645C10.6733 6.79945 10.8458 6.80045 10.9553 6.69095L12.0178 5.62845C12.1333 5.51295 12.1338 5.32045 12.0138 5.20995C10.8328 4.12445 9.25731 3.46145 7.52681 3.46145C5.79631 3.46145 4.22081 4.12445 3.03981 5.20995C2.91981 5.32045 2.92031 5.51295 3.03581 5.62845L4.09831 6.69095C4.20781 6.80045 4.38031 6.79945 4.49581 6.69645C5.30181 5.97695 6.36381 5.53845 7.52681 5.53845C8.68981 5.53845 9.75181 5.97695 10.5578 6.69645Z" fill="white"/>
            <path d="M9.56166 7.6611C9.68716 7.7661 9.68616 7.9616 9.57016 8.0776L7.73216 9.9156C7.61966 10.0281 7.43666 10.0281 7.32416 9.9156L5.48616 8.0776C5.37016 7.9616 5.36866 7.7661 5.49466 7.6611C6.04516 7.2006 6.75416 6.9231 7.52816 6.9231C8.30216 6.9231 9.01066 7.2006 9.56166 7.6611Z" fill="white"/>
        </svg>
        <p>100%</p>
        <svg width="28" height="12" viewBox="0 0 28 12" fill="none" xmlns="http://www.w3.org/2000/svg">
            <g opacity="0.4">
            <mask id="path-1-inside-1_1_52" fill="white">
            <path fill-rule="evenodd" clip-rule="evenodd" d="M5.75335 1H19.3533C20.2799 1 20.9147 1.00078 21.4065 1.04096C21.8866 1.08018 22.1417 1.15192 22.3248 1.24524C22.7482 1.46095 23.0924 1.80516 23.3081 2.22852C23.4014 2.41166 23.4732 2.66673 23.5124 3.14684C23.5526 3.63862 23.5533 4.27341 23.5533 5.2V6.8C23.5533 7.72659 23.5526 8.36138 23.5124 8.85316C23.4732 9.33327 23.4014 9.58834 23.3081 9.77148C23.0924 10.1948 22.7482 10.539 22.3248 10.7548C22.1417 10.8481 21.8866 10.9198 21.4065 10.959C20.9147 10.9992 20.2799 11 19.3533 11H5.75335C4.82676 11 4.19196 10.9992 3.70018 10.959C3.22008 10.9198 2.965 10.8481 2.78187 10.7548C2.3585 10.539 2.0143 10.1948 1.79858 9.77148C1.70527 9.58834 1.63353 9.33327 1.5943 8.85316C1.55412 8.36138 1.55334 7.72659 1.55334 6.8V5.2C1.55334 4.27341 1.55412 3.63862 1.5943 3.14684C1.63353 2.66673 1.70527 2.41166 1.79858 2.22852C2.0143 1.80516 2.3585 1.46095 2.78187 1.24524C2.965 1.15192 3.22008 1.08018 3.70018 1.04096C4.19196 1.00078 4.82676 1 5.75335 1ZM0.553345 5.2C0.553345 3.37983 0.553345 2.46974 0.907574 1.77453C1.21916 1.163 1.71635 0.665817 2.32788 0.354229C3.02309 0 3.93317 0 5.75335 0H19.3533C21.1735 0 22.0836 0 22.7788 0.354229C23.3903 0.665817 23.8875 1.163 24.1991 1.77453C24.5533 2.46974 24.5533 3.37983 24.5533 5.2V6.8C24.5533 8.62017 24.5533 9.53026 24.1991 10.2255C23.8875 10.837 23.3903 11.3342 22.7788 11.6458C22.0836 12 21.1735 12 19.3533 12H5.75335C3.93317 12 3.02309 12 2.32788 11.6458C1.71635 11.3342 1.21916 10.837 0.907574 10.2255C0.553345 9.53026 0.553345 8.62017 0.553345 6.8V5.2ZM27.0533 6C27.0533 5.06808 26.416 4.28503 25.5533 4.06301V7.93699C26.416 7.71497 27.0533 6.93192 27.0533 6Z"/>
            </mask>
            <path d="M21.4065 1.04096L21.3251 2.03764L21.3251 2.03764L21.4065 1.04096ZM22.3248 1.24524L21.8708 2.13624L21.8708 2.13624L22.3248 1.24524ZM23.3081 2.22852L24.1991 1.77453L24.1991 1.77453L23.3081 2.22852ZM23.5124 3.14684L22.5157 3.22827L22.5157 3.22827L23.5124 3.14684ZM23.5124 8.85316L22.5157 8.77173L22.5157 8.77173L23.5124 8.85316ZM23.3081 9.77148L24.1991 10.2255L24.1991 10.2255L23.3081 9.77148ZM22.3248 10.7548L21.8708 9.86376L21.8708 9.86376L22.3248 10.7548ZM21.4065 10.959L21.4879 11.9557L21.4879 11.9557L21.4065 10.959ZM3.70018 10.959L3.61875 11.9557L3.61875 11.9557L3.70018 10.959ZM2.78187 10.7548L3.23586 9.86376L3.23586 9.86376L2.78187 10.7548ZM1.79858 9.77148L0.907573 10.2255L0.907574 10.2255L1.79858 9.77148ZM1.5943 8.85316L2.59098 8.77173L2.59098 8.77173L1.5943 8.85316ZM1.5943 3.14684L2.59098 3.22827L2.59098 3.22827L1.5943 3.14684ZM1.79858 2.22852L2.68959 2.68251L2.68959 2.68251L1.79858 2.22852ZM2.78187 1.24524L3.23586 2.13624L3.23586 2.13624L2.78187 1.24524ZM3.70018 1.04096L3.78161 2.03764L3.78162 2.03764L3.70018 1.04096ZM0.907574 1.77453L1.79858 2.22852L0.907574 1.77453ZM2.32788 0.354229L2.78187 1.24524L2.32788 0.354229ZM22.7788 0.354229L23.2328 -0.536778L23.2328 -0.536778L22.7788 0.354229ZM24.1991 1.77453L23.3081 2.22852L23.3081 2.22852L24.1991 1.77453ZM24.1991 10.2255L23.3081 9.77148L23.3081 9.77148L24.1991 10.2255ZM22.7788 11.6458L22.3248 10.7548L22.3248 10.7548L22.7788 11.6458ZM2.32788 11.6458L2.78187 10.7548L2.78187 10.7548L2.32788 11.6458ZM0.907574 10.2255L0.016567 10.6795L0.0165671 10.6795L0.907574 10.2255ZM25.5533 4.06301L25.8026 3.09457L24.5533 2.77303V4.06301H25.5533ZM25.5533 7.93699H24.5533V9.22697L25.8026 8.90543L25.5533 7.93699ZM19.3533 0H5.75335V2H19.3533V0ZM21.4879 0.0442787C20.9459 -7.92929e-06 20.263 0 19.3533 0V2C20.2968 2 20.8836 2.00156 21.3251 2.03764L21.4879 0.0442787ZM22.7788 0.35423C22.431 0.176994 22.0295 0.0885226 21.4879 0.0442786L21.3251 2.03764C21.7438 2.07184 21.8524 2.12685 21.8708 2.13624L22.7788 0.35423ZM24.1991 1.77453C23.8875 1.163 23.3903 0.665817 22.7788 0.354229L21.8708 2.13624C22.106 2.25608 22.2973 2.44731 22.4171 2.68251L24.1991 1.77453ZM24.5091 3.06541C24.4648 2.52388 24.3763 2.12237 24.1991 1.77453L22.4171 2.68251C22.4265 2.70095 22.4815 2.80959 22.5157 3.22827L24.5091 3.06541ZM24.5533 5.2C24.5533 4.29033 24.5534 3.60745 24.5091 3.06541L22.5157 3.22827C22.5518 3.66978 22.5533 4.2565 22.5533 5.2H24.5533ZM24.5533 6.8V5.2H22.5533V6.8H24.5533ZM24.5091 8.93459C24.5534 8.39255 24.5533 7.70967 24.5533 6.8H22.5533C22.5533 7.7435 22.5518 8.33022 22.5157 8.77173L24.5091 8.93459ZM24.1991 10.2255C24.3763 9.87763 24.4648 9.47612 24.5091 8.93459L22.5157 8.77173C22.4815 9.19041 22.4265 9.29905 22.4171 9.31749L24.1991 10.2255ZM22.7788 11.6458C23.3903 11.3342 23.8875 10.837 24.1991 10.2255L22.4171 9.31749C22.2973 9.55269 22.106 9.74392 21.8708 9.86376L22.7788 11.6458ZM21.4879 11.9557C22.0295 11.9115 22.431 11.823 22.7788 11.6458L21.8708 9.86376C21.8524 9.87315 21.7438 9.92816 21.3251 9.96236L21.4879 11.9557ZM19.3533 12C20.263 12 20.9459 12 21.4879 11.9557L21.3251 9.96236C20.8836 9.99844 20.2968 10 19.3533 10V12ZM5.75335 12H19.3533V10H5.75335V12ZM3.61875 11.9557C4.16079 12 4.84367 12 5.75335 12V10C4.80985 10 4.22313 9.99844 3.78162 9.96236L3.61875 11.9557ZM2.32787 11.6458C2.67572 11.823 3.07723 11.9115 3.61875 11.9557L3.78162 9.96236C3.36293 9.92816 3.25429 9.87315 3.23586 9.86376L2.32787 11.6458ZM0.907574 10.2255C1.21916 10.837 1.71635 11.3342 2.32787 11.6458L3.23586 9.86376C3.00065 9.74392 2.80943 9.55269 2.68959 9.31749L0.907574 10.2255ZM0.597623 8.93459C0.641867 9.47612 0.730339 9.87763 0.907573 10.2255L2.68959 9.31749C2.68019 9.29905 2.62519 9.19041 2.59098 8.77173L0.597623 8.93459ZM0.553345 6.8C0.553345 7.70967 0.553337 8.39255 0.597623 8.93459L2.59098 8.77173C2.55491 8.33022 2.55334 7.7435 2.55334 6.8H0.553345ZM0.553345 5.2V6.8H2.55334V5.2H0.553345ZM0.597623 3.06541C0.553337 3.60745 0.553345 4.29033 0.553345 5.2H2.55334C2.55334 4.2565 2.55491 3.66978 2.59098 3.22827L0.597623 3.06541ZM0.907574 1.77453C0.730339 2.12237 0.641867 2.52388 0.597623 3.06541L2.59098 3.22827C2.62519 2.80959 2.68019 2.70095 2.68959 2.68251L0.907574 1.77453ZM2.32788 0.354229C1.71635 0.665817 1.21916 1.163 0.907574 1.77453L2.68959 2.68251C2.80943 2.44731 3.00065 2.25608 3.23586 2.13624L2.32788 0.354229ZM3.61875 0.0442786C3.07723 0.0885227 2.67572 0.176994 2.32788 0.354229L3.23586 2.13624C3.25429 2.12685 3.36293 2.07184 3.78161 2.03764L3.61875 0.0442786ZM5.75335 0C4.84367 0 4.16079 -7.92941e-06 3.61875 0.0442787L3.78162 2.03764C4.22313 2.00156 4.80985 2 5.75335 2V0ZM0.016567 1.32054C-0.244348 1.83261 -0.349724 2.38019 -0.399056 2.98397C-0.447433 3.57609 -0.446655 4.30642 -0.446655 5.2H1.55334C1.55334 4.27341 1.55412 3.63862 1.5943 3.14684C1.63353 2.66673 1.70527 2.41166 1.79858 2.22852L0.016567 1.32054ZM1.87388 -0.536778C1.0742 -0.129316 0.424029 0.520851 0.016567 1.32054L1.79858 2.22852C2.0143 1.80516 2.3585 1.46095 2.78187 1.24524L1.87388 -0.536778ZM5.75335 -1C4.85976 -1 4.12943 -1.00078 3.53732 -0.9524C2.93353 -0.903069 2.38596 -0.797693 1.87388 -0.536778L2.78187 1.24524C2.965 1.15192 3.22008 1.08018 3.70018 1.04096C4.19196 1.00078 4.82676 1 5.75335 1V-1ZM19.3533 -1H5.75335V1H19.3533V-1ZM23.2328 -0.536778C22.7207 -0.797693 22.1732 -0.903069 21.5694 -0.9524C20.9773 -1.00078 20.2469 -1 19.3533 -1V1C20.2799 1 20.9147 1.00078 21.4065 1.04096C21.8866 1.08018 22.1417 1.15192 22.3248 1.24524L23.2328 -0.536778ZM25.0901 1.32054C24.6827 0.520852 24.0325 -0.129316 23.2328 -0.536778L22.3248 1.24524C22.7482 1.46095 23.0924 1.80516 23.3081 2.22852L25.0901 1.32054ZM25.5533 5.2C25.5533 4.30642 25.5541 3.57609 25.5057 2.98397C25.4564 2.38019 25.351 1.83261 25.0901 1.32054L23.3081 2.22852C23.4014 2.41166 23.4732 2.66673 23.5124 3.14684C23.5526 3.63862 23.5533 4.27341 23.5533 5.2H25.5533ZM25.5533 6.8V5.2H23.5533V6.8H25.5533ZM25.0901 10.6795C25.351 10.1674 25.4564 9.61981 25.5057 9.01603C25.5541 8.42391 25.5533 7.69358 25.5533 6.8H23.5533C23.5533 7.72659 23.5526 8.36138 23.5124 8.85316C23.4732 9.33327 23.4014 9.58834 23.3081 9.77148L25.0901 10.6795ZM23.2328 12.5368C24.0325 12.1293 24.6827 11.4791 25.0901 10.6795L23.3081 9.77148C23.0924 10.1948 22.7482 10.539 22.3248 10.7548L23.2328 12.5368ZM19.3533 13C20.2469 13 20.9773 13.0008 21.5694 12.9524C22.1732 12.9031 22.7207 12.7977 23.2328 12.5368L22.3248 10.7548C22.1417 10.8481 21.8866 10.9198 21.4065 10.959C20.9147 10.9992 20.2799 11 19.3533 11V13ZM5.75335 13H19.3533V11H5.75335V13ZM1.87388 12.5368C2.38596 12.7977 2.93353 12.9031 3.53732 12.9524C4.12943 13.0008 4.85976 13 5.75335 13V11C4.82676 11 4.19196 10.9992 3.70018 10.959C3.22008 10.9198 2.965 10.8481 2.78187 10.7548L1.87388 12.5368ZM0.0165671 10.6795C0.424029 11.4791 1.0742 12.1293 1.87389 12.5368L2.78187 10.7548C2.3585 10.539 2.0143 10.1948 1.79858 9.77148L0.0165671 10.6795ZM-0.446655 6.8C-0.446655 7.69358 -0.447433 8.42391 -0.399056 9.01603C-0.349724 9.61981 -0.244348 10.1674 0.016567 10.6795L1.79858 9.77148C1.70527 9.58834 1.63353 9.33327 1.5943 8.85316C1.55412 8.36138 1.55334 7.72659 1.55334 6.8H-0.446655ZM-0.446655 5.2V6.8H1.55334V5.2H-0.446655ZM25.3041 5.03145C25.7356 5.14251 26.0533 5.53541 26.0533 6H28.0533C28.0533 4.60075 27.0963 3.42755 25.8026 3.09457L25.3041 5.03145ZM26.5533 7.93699V4.06301H24.5533V7.93699H26.5533ZM26.0533 6C26.0533 6.46459 25.7356 6.85749 25.3041 6.96855L25.8026 8.90543C27.0963 8.57245 28.0533 7.39925 28.0533 6H26.0533Z" fill="white" mask="url(#path-1-inside-1_1_52)"/>
            </g>
            <rect x="2.55334" y="2" width="20" height="8" rx="1.5" fill="white"/>
        </svg>                 
</div>
</div>
<!-- Список приложений -->
<div class="applist">

    <!-- Фракция -->
    <div class="blockapp" on:click={handleFractionClick}>
      <img
        src={
          userData.FractionID > 0
            ? `http://cdn.piecerp.ru/cloud/img/fractionLogo/Fraction_${userData.FractionID}.svg`
            : 'http://cdn.piecerp.ru/cloud/img/fractionLogo/no-fraction.svg'
        }
        alt="Fraction Icon"
      />
      <p>
        {#if userData.FractionID > 0}
          {fraction[userData.FractionID] || 'Фракция'}
        {:else}
          Фракция
        {/if}
      </p>
    </div>

    <!-- Организация -->
    <div class="blockapp" on:click={handleOrganizationClick}>
      <img 
        src="http://cdn.piecerp.ru/cloud/icons/no-family.svg"
        alt="Organization Icon"
      />
      <p>Организация</p>
    </div>

    <!-- Дом -->
    <div class="blockapp" on:click={handleHouseClick}>
      <img src="http://cdn.piecerp.ru/cloud/icons/Home.svg" alt=""/>
      <p>Мой Дом</p>
    </div>

    <!-- Бизнес -->
    <div class="blockapp" on:click={handleBizClick}>
      <img src="http://cdn.piecerp.ru/cloud/icons/business.svg" alt=""/>
      <p>Мой Бизнес</p>
    </div>

    <!-- Маркетплейс -->
    <div class="blockapp" on:click={() => onSelectPage("marketPlace")}>
      <img src="http://cdn.piecerp.ru/cloud/icons/marketplace.svg" alt=""/>
      <p>Маркетплейс</p>
    </div>

</div>

<!-- Нижняя панель (dock) -->
<div class="dock">

    <!-- Бизнес (dock) -->
    <div class="blockapp" on:click={handleBizClick}>
      <img src="http://cdn.piecerp.ru/cloud/icons/business.svg" alt=""/>
    </div>

    <!-- Дом (dock) -->
    <div class="blockapp" on:click={handleHouseClick}>
      <img src="http://cdn.piecerp.ru/cloud/icons/Home.svg" alt=""/>
    </div>

</div>

