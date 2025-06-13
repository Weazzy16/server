<script>
    import Page1 from "./elements/work/no_work.svelte";
    import Page2 from "./elements/work/electrician.svelte";
    import Page3 from "./elements/work/lawnmower.svelte";
    import Page4 from "./elements/work/bus.svelte";
    import Page5 from "./elements/work/mechanic.svelte";
    import Page6 from "./elements/work/taxi.svelte";
    import Page7 from "./elements/work/trucker.svelte";
    import Page8 from "./elements/work/postmen.svelte";
    import Page9 from "./elements/work/coater.svelte";
    // import Page10 from "./elements/page3.svelte";
    

    const Views = {
        Page1,
        Page2,
        Page3,
        Page4,
        Page5,
        Page6,
        Page7,
        Page8,
        Page9,
        // Page10,
    }

    let onSelectedView2 = "Page1";

</script>

<div class="menurules">
    <div class="rulesleft">
        <div class="headrules">
            <h1>ВЫБОР КАТЕГОРИИ</h1>
        </div>
        <div class="ruleslist">
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page1"} class:active={onSelectedView2 == "Page1"}>
                <p>Подработки</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page2"} class:active={onSelectedView2 == "Page2"}>
                <p>Электрик</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page3"} class:active={onSelectedView2 == "Page3"}>
                <p>Газонокосильщик</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page4"} class:active={onSelectedView2 == "Page4"}>
                <p>Водитель автобуса</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page5"} class:active={onSelectedView2 == "Page5"}>
                <p>Механик</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page6"} class:active={onSelectedView2 == "Page6"}>
                <p>Таксист</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page7"} class:active={onSelectedView2 == "Page7"}>
                <p>Дальнобойщик</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page8"} class:active={onSelectedView2 == "Page8"}>
                <p>Почтальон</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page9"} class:active={onSelectedView2 == "Page9"}>
                <p>Инкассатор</p>
            </div>
        </div>
    </div>
    <div class="rulesright">
        <svelte:component this={Views[onSelectedView2]}/>
    </div>
</div>