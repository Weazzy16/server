<script>
    import { charData } from 'store/chars';
    import {executeClientAsyncToGroup, executeClientToGroup} from "api/rage";
    import {addListernEvent, hasJsonStructure} from "api/functions";
    import {selectedImage, selectedImageFunc} from "@/views/player/hudevo/phonenew/stores";
    import {messageType} from "@/views/player/hudevo/phonenew/components/messages/data";

    export let isCreate;

    let avatar = "";
    let text = "";
    let type = 0;
    let isVisible = 0;

    let profile = [];

    const getProfile = () => {
        executeClientAsyncToGroup("tinder.getProfile").then((result) => {
            if (hasJsonStructure(result)) {
                profile = JSON.parse(result);
                avatar = profile.avatar;
                text = profile.text;
                type = profile.type;
                isVisible = profile.isVisible;
            }
        });
    }

    getProfile ();
    addListernEvent ("phone.tinder.getProfile", getProfile)

    let isEdit;

    export let setLoad;

    const onEdit = () => isEdit = true;

    const onSave = () => {
        if (avatar == true) {
            return window.notificationAdd(4, 9, "Foto hochgeladen", 3000);
        }
        executeClientToGroup("tinder.save", avatar, text, type, isVisible);
        isEdit = false;
        if (!isCreate)
            setLoad ();
    }

    const sendImage = (link) => {
        avatar = link;
    }

    addListernEvent ("cameraLink", sendImage)
    const onSendImage = () => {
        selectedImage.set(true)
        selectedImageFunc.set(sendImage)
    }

    const maxSymbol = 150;
    import { onInputFocus, onInputBlur } from "@/views/player/hudevo/phonenew/data";
</script>

{#if isCreate && !isEdit}
<div class="newphone__maps_headertitleGPS newphone__project_padding16">Dein Profil:</div>
<div class="newphone__ads_list auction big">
    <div class="tinder--card">
        <div class="tindimage" style="background-image: url({profile.avatar})"></div>
        <div class="tinder__name newphone__project_padding16">{$charData.Name}</div>
        <!--<div class="tinder__name gray newphone__project_padding16">13 км. от вас</div>-->
        <div class="tinder__line"></div>
        <div class="tinder__descr newphone__project_padding16">{profile.text}</div>
    </div>
    <div class="newphone__project_button tinder" on:keypress={() => {}} on:click={onEdit}>Bearbeiten</div>
</div>
{:else}
    <div class="newphone__project_padding16">
        <div class="newphone__maps_headertitleGPS">Profil bearbeiten</div>
        <div class="newphone__maps_headertitleGPS">Beschreibung:</div>
        <textarea placeholder="Text eingeben" class="newphone__message_textarea profile" bind:value={text} maxlength={maxSymbol} on:focus={onInputFocus} on:blur={onInputBlur}></textarea>
        <div class="newphone__maps_headertitleGPS">Ich suche:</div>
        <div class="box-between w-100 mb-10">
            <div>Mann</div>
            <div class="newphone__checkbox" on:keypress={() => {}} on:click={() => type = 0}>
                <input class="styled-checkbox viol" id="isMan" type="checkbox" disabled checked={type === 0}>
                <label class="styled-checkbox1" for="isMan"></label>
            </div>
        </div>
        <div class="box-between w-100 mb-10">
            <div>Frau</div>
            <div class="newphone__checkbox" on:keypress={() => {}} on:click={() => type = 1}>
                <input class="styled-checkbox viol" id="isWoman" type="checkbox" disabled checked={type === 1}>
                <label class="styled-checkbox1" for="isWoman"></label>
            </div>
        </div>
        <div class="box-between w-100 mb-10">
            <div>Freunde</div>
            <div class="newphone__checkbox" on:keypress={() => {}} on:click={() => type = 2}>
                <input class="styled-checkbox viol" id="isFriends" type="checkbox" disabled checked={type === 2}>
                <label class="styled-checkbox1" for="isFriends"></label>
            </div>
        </div>
        <div class="box-between w-1">
            <div>Profil veröffentlichen</div>
            <div class="sound__input-block switch-box">
                <label class="switch" on:keypress={() => {}} on:click={() => isVisible = !isVisible}>
                    <input type="checkbox" checked={isVisible} disabled >
                    <span class="slider round"></span>
                </label>
            </div>
        </div>
        <div class="newphone__project_button tinder" on:keypress={() => {}} on:click={onSendImage}>Foto auswählen</div>
        <div class="newphone__project_button tinder" on:keypress={() => {}} on:click={onSave}>Speichern</div>
    </div>
{/if}
