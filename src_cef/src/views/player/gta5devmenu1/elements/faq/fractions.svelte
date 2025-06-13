<script>
    import Gang from './elements/fractions/gang.svelte';
    import Mafia from './elements/fractions/mafia.svelte';
    import TheLost from './elements/fractions/thelost.svelte';

    const Views = {
        Gang,
        Mafia,
        TheLost,
    }

    let onSelectedView2 = "Page1";

</script>

<div class="menurules">
    <div class="rulesleft">
        <div class="headrules">
            <h1>ВЫБОР КАТЕГОРИИ</h1>
        </div>
        <div class="ruleslist">
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Gang"} class:active={onSelectedView2 == "Gang"}>
                <p>Банды</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Mafia"} class:active={onSelectedView2 == "Mafia"}>
                <p>Мафии</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "TheLost"} class:active={onSelectedView2 == "TheLost"}>
                <p>Байкеры</p>
            </div>
        </div>
    </div>
    <div class="rulesright">
        <svelte:component this={Views[onSelectedView2]}/>
    </div>
</div>