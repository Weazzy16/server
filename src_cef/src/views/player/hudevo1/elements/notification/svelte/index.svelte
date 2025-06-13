<script>
    import { fly, fade } from 'svelte/transition';
    // import './main.sass';
    import succ from './succ.png'
    import { format } from 'api/formatter'
    import { storeSettings } from 'store/settings'
    import { executeClient } from 'api/rage'


   let notifications = [

   ]; 

    window.notification = (json) => {
        if (json) {
            json = JSON.parse(json);
            for (let i = 0; i < json.length; i++) {
                if (typeof json [i] === "object")
                    json [i].text = format("parse", json [i].text);
            }
        }

        notifications = json;
    }

    window.notificationAdd = (type, _, msg, time) => {
        executeClient ("notify", type, _, msg, time);
    }

    const onDelete = (index) => {
        executeClient ("notifyClear", index);
    }

    export let night;

</script>


{#if notifications.length > 0}
    <div class="newhud__notification list" class:night={night}>
        <div class="newhud__notification_list">
            {#each new Array($storeSettings.notifCount <= 1 ? 1 : $storeSettings.notifCount) as _, index}
                {#if notifications[index]}
                <!-- <div class="hudevo__notification {notifications[index].type}" in:fly={{ y: 50, duration: 350 }} out:fly={{ y: 50, duration: 350 }} on:click={() => onDelete(index)}>
                    <div class="hudevo__notification_title">Уведомление</div>
                    <div class="hudevo__notification_count">
                        <div class="activeline {notifications[index].type}" style={
                            `width: ${notifications[index].percent}%;` + `transition-duration:${notifications[index].percent === 0 ? notifications[index].timeout : 0}ms;`}></div>
                    </div>
                    <div class="hudevo__notification_text">{@html notifications[index].elements}</div>
                </div> -->
                <div id="notify">
                    <div class="notify_block">
                        <header><div class="notify_block_rumble"></div>Уведомление</header>
                        <p>{@html notifications[index].elements}</p>
                        <div class="notify_block_line"><div class="notify_block_line_active {notifications[index].type}"></div></div>
                    </div>
                </div>
                {/if}
            {/each}
        </div>
        <div class="succ"><img src={succ} alt="succ" style="padding-top: 34.5%;padding-left: 2.45%;"></div>
    </div>
{/if}

<style>
    @import url('https://fonts.googleapis.com/css2?family=Montserrat:wght@100;500;700&display=swap');
    #notify {
    position: absolute;
    bottom: 1vw;
    left:7%;
    font-family: 'Montserrat';
    transform: translate(-50%,0);
    color: white;
    pointer-events: none;
}
.notify_block.error {
    background: linear-gradient(135deg, rgb(27, 5, 5), rgb(27, 5, 5,.40));
}
.notify_block.error .notify_block_line_active,
.notify_block.error .notify_block_rumble {
    background: #ec3d40;
}
.notify_block.alert {
    background: linear-gradient(135deg, rgb(27, 12, 5), rgb(27, 12, 5,.40));
}
.notify_block.alert .notify_block_line_active,
.notify_block.alert .notify_block_rumble {
    background: #ec623d;
}
.notify_block.succes {
    background: linear-gradient(135deg, rgb(7, 27, 5), rgb(7, 27, 5,.40));
}
.notify_block.succes .notify_block_line_active,
.notify_block.succes .notify_block_rumble {
    background: #84ea4a;
}
.notify_block.info {
    background: linear-gradient(135deg, rgb(5, 17, 27), rgb(5, 17, 27,.40));
}
.notify_block.info .notify_block_line_active,
.notify_block.info .notify_block_rumble {
    background: #3579e8;
}
.notify_block.warn {
    background: linear-gradient(135deg, rgb(27, 20, 5), rgb(27, 20, 5,.40));
}
.notify_block.warn .notify_block_line_active,
.notify_block.warn .notify_block_rumble {
    background: #e8b335;
}
.notify_block {
    position: relative;
    width: 233px;
    margin-left: 188px;
    padding: .75vw;
    background: rgba(36, 255, 0, 0.53);
    border-radius: 7px;
    margin-bottom: 250px;
}
.notify_block header {
    position: relative;
    font-size: 13px;
    font-weight: 700;
    margin-top: -3px;
    display: flex;
}
.notify_block_rumble {
    position: relative;
    width: .18vw;
    height: .8vw;
    margin-right: .5vw;
    margin-top: .1vw;
    color: #fff;
    margin-left: .25vw;
    background: #958cda;
    transform: rotate(45deg);
    border-radius: .15vw;
}
.notify_block p {
    font-size: 10px;
    color: #fff;
    opacity: .5;
    margin-top: 1px;
}
.notify_block_line {
    position: relative;
    left: -5%;
    top: 0;
    width: 13.3vw;
    height: 1.7px;
    border-radius: .25vw;
    overflow: hidden;
    background: rgba(0, 255, 87,.30);
    margin-top: 7.5%;
}
.notify_block_line_active {
    background: #00FF57;
    position: absolute;
    width: 100%;
    height: 100%;
    transition: width .2s;
}

.notification-enter-active {
    opacity: 0;
    animation: NotificationEnter .4s forwards;
}

.notification-leave-active {
    transition: opacity .5s;
    opacity: 0;
}

@keyframes NotificationEnter {
    from {
        opacity: 0;
        transform: translate(0, 50%);
    }
    to {
        opacity: 1;
        transform: translate(0, 0);
    }
}
</style>