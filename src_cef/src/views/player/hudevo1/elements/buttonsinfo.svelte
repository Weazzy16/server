<script>
    import { translateText } from 'lang'
    import { charIsPet } from 'store/chars'
    import { fly } from 'svelte/transition';
    import { inVehicle, isInputToggled, isHelp } from 'store/hud'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
	import router from 'router';

    let isHint = false;
    window.hudStore.isHint = (value) => isHint = value;

    let HideHelp = false;
    window.hudStore.HideHelp = (value) => HideHelp = value;
</script>
{#if !$charIsPet && $isHelp}
    <div class="keys-block-main">
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[12]]}</div>
            <div class="key-block__text-meaning">Инвентарь</div>
        </div>
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[36]]}</div>
            <div class="key-block__text-meaning">Голосовой чат</div>
        </div>
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[9]]}</div>
            <div class="key-block__text-meaning">Взаимодействие</div>
        </div>
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[30]]}</div>
            <div class="key-block__text-meaning">Телефон</div>
        </div>
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[27]]}</div>
            <div class="key-block__text-meaning">Скрыть HUD</div>
        </div>
        <div class="key-block">
            <div class="key-block-value">{keysName[$keys[45]]}</div>
            <div class="key-block__text-meaning">Указать пальцем</div>
        </div>
        {#if $inVehicle}
        <div class="key-block">
            <div class="key-block-value active">{keysName[$keys[53]]}</div>
            <div class="key-block__text-meaning">Ремень безопасности</div>
        </div>
        <div class="key-block">
            <div class="key-block-value active">{keysName[$keys[38]]}</div>
            <div class="key-block__text-meaning">Завести мотор</div>
        </div>
        {/if}
    </div>
{/if}