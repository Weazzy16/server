<script>
    import Page1 from "./general/page1.svelte";
    import Page2 from "./general/page2.svelte";
    import Page3 from "./general/page3.svelte";
    import Page4 from "./general/page4.svelte";
    import Page5 from "./general/page5.svelte";
    import Page6 from "./general/page6.svelte";
    import Page7 from "./general/page7.svelte";
    import Page8 from "./general/page8.svelte";
    import Page9 from "./general/page9.svelte";
    import Page10 from "./general/page10.svelte";
    import Page11 from "./general/page11.svelte";

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
        Page10,
        Page11,
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
                <p>Основные правила проекта</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page2"} class:active={onSelectedView2 == "Page2"}>
                <p>Правила проверки на стороннее ПО</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page3"} class:active={onSelectedView2 == "Page3"}>
                <p> Правила нападения / обороны территории Форта-Занкудо</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page4"} class:active={onSelectedView2 == "Page4"}>
                <p>Правила ограблений и похищений</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page5"} class:active={onSelectedView2 == "Page5"}>
                <p>Общие правила семейных организаций</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page6"} class:active={onSelectedView2 == "Page6"}>
                <p>Правила военного положения</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page7"} class:active={onSelectedView2 == "Page7"}>
                <p>Правила игровых зон</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page8"} class:active={onSelectedView2 == "Page8"}>
                <p>Правила и обязанности администраторов</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page9"} class:active={onSelectedView2 == "Page9"}>
                <p>Правила для лидеров фракций</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page10"} class:active={onSelectedView2 == "Page10"}>
                <p>Правила войны за AirDrop</p>
            </div>
            <div class="rulesblock" on:keypress={() => {}} on:click={() => onSelectedView2 = "Page11"} class:active={onSelectedView2 == "Page11"}>
                <p>Правила перехвата поставок</p>
            </div>
        </div>
    </div>
    <div class="rulesright">
        <svelte:component this={Views[onSelectedView2]}/>
    </div>
</div>