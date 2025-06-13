<script>
    import './main.sass';

    import Chat from './chat.svelte'
    import Guides from './guides.svelte'

    const Views = {
        Chat,
        Guides
    }

    let selectView = "Chat"

</script>

<div id ="support">
    <div class="support__nav">
        <div class="support__title">Get help</div>
        <div class="support__subtitle" class:active={selectView === "Chat"} on:keypress={() => {}} on:click={() => selectView = "Chat"}>— Online chat with administrator</div>
        <div class="support__subtitle" class:active={selectView === "Guides"} on:keypress={() => {}} on:click={() => selectView = "Guides"}>— Gaida</div>
    </div>
    <svelte:component this={Views[selectView]}/>
    <div class="support__nav"></div>
</div>