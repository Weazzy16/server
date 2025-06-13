<script>
    import { setGroup, executeClient, executeClientToGroup, executeClientAsyncToGroup } from 'api/rage'

    export let selectView;

    import { translateText } from 'lang'

    import './main.sass';
    import './assets/style.css'
    import './assets/fonts/fonts/style.css'

    import Main from './elements/main/main.svelte'
    import Logs from './elements/main/logs.svelte'
    import Ranks from './elements/ranks/index.svelte'
    import Members from './elements/members/index.svelte'
    import Squads from './elements/squads/index.svelte'//squadslist

    import Accesses from './elements/accesses/index.svelte'



    import Tasks from './elements/tasks/index.svelte'
    import Form from './elements/form/form.svelte'
    import Parking from './elements/parking/parking.svelte'
    import BestPlayers from './elements/bestplayers/bestplayers.svelte'
    import Complaints from './elements/complaints/complaints.svelte'
    import Weapons from './elements/weapons/weapons.svelte'
    import BizWar from './elements/bizwar/bizwar.svelte'
    import Online from './menu/online.svelte'

    const Views = {
        Main,
        Logs,
        Tasks,
        Members,
        Squads,
        Ranks,
        Form,
        Accesses,
        Parking,
        BestPlayers,
        Complaints,
        Weapons,
        BizWar
    }

    import Input from './popup/input/index.svelte'
    import Confirm from './popup/confirm/index.svelte'
    import List from './popup/list/index.svelte'

    import SquadRanks from './popups/squadranks/index.svelte'


    import BizInfo from './popups/bizinfo/index.svelte'
    import Upgrade from './popups/upgrade/index.svelte'
    import WeaponsPopup from './popups/weapons/index.svelte'
    import NewLeaderPopup from './popups/newleader/index.svelte'

    
    const Popups = {
        Input,
        Confirm,
        List,

        Upgrade,
        SquadRanks,

        BizInfo,
        WeaponsPopup,
        NewLeaderPopup
    }

    import { setTableType, selectTableView, setView, selectPopup, setPopup, isOrganization, isFraction, onInputBlur } from './data'

    let isLoad = false;
    const onLoad = () => {
        isLoad = false;
        setTableType (selectView);

        if (selectView === "Fractions")
            setGroup('.frac.main.')
        else
            setGroup('.org.main.')

        setView ("Main");

        setPopup ();
        setTimeout(() => {
            isLoad = true;
        }, 50);
    }

    $: if (selectView)
        onLoad();

    let selectedPopup = ""
    selectPopup.subscribe(value => {
        selectedPopup = value;
    });

    const HandleKeyDown = (event) => {
        const { keyCode } = event;
        if (keyCode === 27 && Popups[selectedPopup]) {
            setPopup ();
        }
    }

    let selectedTableView = "";
    selectTableView.subscribe(value => {
        selectedTableView = value;
    });


    const onInvate = (value) => {
        executeClientToGroup('invitePlayer', value);
    }

    const onOpenPopupInvate = () => {
        setPopup ("Input", {
            headerTitle: "Invitation",
            headerIcon: "fractionsicon-members",
            input: {
                title: "ID or name surname",
                placeholder: "Enter the player's data",
                maxLength: 36,
            },
            button: translateText('popups', 'Подтвердить'),
            callback: onInvate
        })
    }

    let tableInfo = {};

    const onTableInfo = (json) => {
        tableInfo = JSON.parse(json);
        getSettings();
    }
    import { addListernEvent } from "api/functions";
    addListernEvent ("table.tableInfo", onTableInfo)

    let settings = {};
    const getSettings = () => {
        executeClientAsyncToGroup("getSettings").then((result) => {
            if (result && typeof result === "string")
                settings = JSON.parse(result);
        });
    }
    const onLeaveCallback = () => {
        executeClientToGroup('leave');
    }

    const onLeave = () => {
        setPopup ("Confirm", {
            headerTitle: "Dismissal",
            headerIcon: "fractionsicon-members",
            text: `Do you really want to leave the organization?`,
            button: 'Leave',
            callback: onLeaveCallback
        })
    }

    const onDisolverCallback = () => {
        executeClientToGroup('disolver');
    }

    const onDisolver = () => {
        setPopup ("Confirm", {
            headerTitle: "Dissolution",
            headerIcon: "fractionsicon-members",
            text: `Do you really want to dissolve the organization?`,
            button: 'Dissolve',
            callback: onDisolverCallback
        })
    }

    const onFamilyZone = () => {
        executeClient ("client.familyZoneOpen")
    }

    import { onDestroy } from 'svelte';
    onDestroy(() => {
        onInputBlur ();
    })

</script>

{#if Popups[selectedPopup]}
    <svelte:component this={Popups[selectedPopup]} />
{/if}

<svelte:window on:keyup={HandleKeyDown} />

{#if isLoad}
<div id="fractions" class="pt-63">
    <div class="fractions__left">
        <div class="fractions__left_title">{tableInfo.name}</div>
        <div class="fractions__left_subtitle">{translateText('player1', 'Меню управления')}</div>
        {#if settings.invite}
        <div class="fractions__button_big" on:keypress={() => {}} on:click={onOpenPopupInvate}>
            <span class="fractionsicon-plus"></span>{translateText('player1', 'Пригласить')}
        </div>
        {/if}
        <div class="fractions__menu_element" class:active={selectedTableView === "Main"} on:keypress={() => {}} on:click={() => setView ("Main")}>
            <span class="fractionsicon-fractions"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Главная страница')}</div>
        </div>
        {#if isFraction()}
        <div class="fractions__menu_element" class:active={selectedTableView === "Tasks"} on:keypress={() => {}} on:click={() => setView ("Tasks")}>
            <span class="fractionsicon-tasks"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Задания')}</div>
        </div>
        {/if}
        <div class="fractions__menu_element" class:active={selectedTableView === "Members"} on:keypress={() => {}} on:click={() => setView ("Members")}>
            <span class="fractionsicon-members"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Состав')}</div>
        </div>
        <div class="fractions__menu_element" class:active={selectedTableView === "Squads"} on:keypress={() => {}} on:click={() => setView ("Squads")}>
            <span class="fractionsicon-squads"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Отряды')}</div>
        </div>
        {#if settings.isLeader}
        <div class="fractions__menu_element mb-36" class:active={selectedTableView === "Ranks"} on:keypress={() => {}} on:click={() => setView ("Ranks")}>
            <span class="fractionsicon-rank"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Ранги')}</div>
        </div>
        {/if}
        {#if settings.clothesEdit}
        <div class="fractions__menu_element" class:active={selectedTableView === "Form"} on:keypress={() => {}} on:click={() => setView ("Form")}>
            <span class="fractionsicon-form"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Форма')}</div>
        </div>
        {/if}
        {#if settings.isLeader}
        <div class="fractions__menu_element" class:active={selectedTableView === "Accesses"} on:keypress={() => {}} on:click={() => setView ("Accesses")}>
            <span class="fractionsicon-complaints"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Доступы')}</div>
        </div>
        {/if}
        <div class="fractions__menu_element" class:active={selectedTableView === "Parking"} on:keypress={() => {}} on:click={() => setView ("Parking")}>
            <span class="fractionsicon-parking"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Парковка')}</div>
        </div>
        <!--<div class="fractions__menu_element" class:active={selectedTableView === "Weapons"} on:click={() => setView ("Weapons")}>
            <span class="fractionsicon-stock"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Наборы оружия')}</div>
        </div>
        <div class="fractions__menu_element" class:active={selectedTableView === "BestPlayers"} on:click={() => setView ("BestPlayers")}>
            <span class="fractionsicon-best"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Лучшие участники')}</div>
        </div>
        <div class="fractions__menu_element mb-36" class:active={selectedTableView === "Complaints"} on:click={() => setView ("Complaints")}>
            <span class="fractionsicon-warning"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Список жалоб')}</div>
        </div>-->
        {#if settings.familyZone}
        <div class="fractions__menu_element" on:keypress={() => {}} on:click={onFamilyZone}>
            <span class="fractionsicon-gov"></span>
            <div class="fractions__menu_text">{translateText('player1', 'Война за территории')}</div>
        </div>
        {/if}
        {#if !settings.isLeader}
            <div class="fractions__menu_element" on:keypress={() => {}} on:click={onLeave}>
                <span class="fractionsicon-exit"></span>
                <div class="fractions__menu_text">{isOrganization() ? translateText('player1', 'Покинуть организацию') : translateText('player1', 'Покинуть фракцию')}</div>
            </div>
        {:else if isOrganization()}
            <div class="fractions__menu_element" on:keypress={() => {}} on:click={onDisolver}>
                <span class="fractionsicon-exit"></span>
                <div class="fractions__menu_text">{translateText('player1', 'Распустить')}</div>
            </div>
        {/if}

        <Online />

    </div>
    <div class="fractions__right">
        <svelte:component this={Views[selectedTableView]} />
    </div>
</div>
{/if}