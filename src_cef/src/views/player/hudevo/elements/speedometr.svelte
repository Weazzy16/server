<script>
    import { translateText } from 'lang'

    import { fly } from 'svelte/transition';
    import { inVehicle, isInputToggled } from 'store/hud'
    import keys from 'store/keys'
    import keysName from 'json/keys.js'
import './assets/css/index2.css';
    let speed = 9;
    let gear = ''
    let maxSpeed = 100;
    let fuel = 0;

    let speedDeg = 0;
    let fuelDeg = 0;

    let speedometerTest;
    let fuelTest;

    const GetCircle = (element, r, maxWidth = 160) => {
        if (!element) return r;
        element = element.getBoundingClientRect();
        return r / 100 * (element.width * 100 / maxWidth);
    }

    let FuelProcent = 0;
    const maxFuel = 500;



    window.vehicleState = {}


    window.vehicleState.gear = (value) => {
        gear = value;
        //speedDeg = !(speed > maxSpeed) ? (speed * 185 / maxSpeed) : (maxSpeed * 185 / maxSpeed);
    }

    window.vehicleState.speed = (value) => {
        speed = value;
        //speedDeg = !(speed > maxSpeed) ? (speed * 185 / maxSpeed) : (maxSpeed * 185 / maxSpeed);
    }

    window.vehicleState.maxSpeed = (value) => {
        maxSpeed = value * 1.15;
        //speedDeg = !(speed > maxSpeed) ? (speed * 185 / maxSpeed) : (maxSpeed * 185 / maxSpeed);
    }

    window.vehicleState.fuel = (value) => {
        fuel = value;
        //fuelDeg = !(fuel > maxFuel) ? (fuel * 62 / maxFuel) : (maxFuel * 62 / maxFuel);
        FuelProcent = value;
    }

    // Simple color interpolator for Speed.
    const InterpolateColor = (start, end, steps, count) => {
        var
            s = start,
            e = end,
            final = s + (((e - s) / steps) * count);
        return Math.floor(final);
    }

    const Color = (_r, _g, _b) => {
        return {
            r: _r,
            g: _g,
            b: _b
        };
    }

    const getSpeedColor = (value, max) => {
        let speed_percent = Math.floor(value / max * 100);
        if(speed_percent > 100) speed_percent = 100;

        let
            start = Color(255, 255, 255),
            end = Color(225, 228, 66);
    
        if (speed_percent > 50) {
            speed_percent = speed_percent % 51;
            start = Color(225, 228, 66);
            end = Color(228, 66, 66);
        }

        const start_colors = start;
        const end_colors = end;
        
        const 
            r = InterpolateColor(start_colors.r, end_colors.r, 50, speed_percent),
            g = InterpolateColor(start_colors.g, end_colors.g, 50, speed_percent),
            b = InterpolateColor(start_colors.b, end_colors.b, 50, speed_percent);

        return `rgb(${r}, ${g}, ${b})`;          
    }

    let autoPilot = false;
    window.vehicleState.autoPilot = (value) => autoPilot = value;

    let belt = false;
    window.vehicleState.belt = (value) => belt = value;

    let cruiseControl = false;
    window.vehicleState.cruiseControl = (value) => cruiseControl = value;

    let rightIL = false;
    window.vehicleState.rightIL = (value) => rightIL = value;

    let leftIL = false;
    window.vehicleState.leftIL = (value) => leftIL = value;

    let doors = false;
    window.vehicleState.doors = (value) => doors = value;

    let engine = false;
    window.vehicleState.engine = (value) => engine = value;

    let isToggledVehicleHud = true;
    window.vehicleState.isToggledVehicleHud = (value) => isToggledVehicleHud = value;


</script>


{#if $inVehicle && isToggledVehicleHud}
<div class="timebars-with-speedo" in:fly={{ x: 50, duration: 500 }} out:fly={{ x: 50, duration: 250 }}>
    <div class="timebars"></div>
    <div class="carhud">
        <div class="carhud-radial-shadow"></div>
        <div class="carhud-container-fourth">
            <div class="carhud-container-fourth__kmh">KM/H</div>
            <div class="carhud-container-fourth__speed">
                {#if gear == 0}
                <div class="carhud-container-fourth__speed__gear">N</div>
                {/if}
                {#if gear != 0}
                <div class="carhud-container-fourth__speed__gear">{gear}</div>
                {/if}
                <div class="carhud-container-fourth__speed__value">
                    {#if speed <= 0}
                    <div class="grey-color">0</div>
                    {/if}
                    {#if speed < 10}
                    <div class="grey-color">0</div>
                    {/if}
                    {#if speed < 100}
                    <div class="grey-color">0</div>
                    {/if}
                    {#if speed != 0}
                    {speed}
                    {/if}
                </div>
            </div>
            <div class="carhud-container-fourth__additional">
                <div class="carhud-container-fourth__additional__icons">
                    <svg data-v-67f0a894="" width="2.962962962962963vh" height="2.4074074074074074vh" viewBox="0 0 32 26" fill="none" xmlns="http://www.w3.org/2000/svg" class={ 'carhud-container-fourth__additional__icons__emergencyGang ' }>
                        <path d="M17.1556 16.157C17.1556 16.6999 16.7947 17.1394 16.345 17.1394H15.6104C15.1608 17.1394 14.7998 16.6999 14.7998 16.157V5.83587C14.7998 5.29299 15.1608 4.85352 15.6104 4.85352H16.345C16.7947 4.85352 17.1556 5.29299 17.1556 5.83587V16.157Z" fill="white"></path>
                        <path d="M17.181 20.164C17.181 20.7069 16.82 21.1528 16.3704 21.1528H15.6104C15.1608 21.1528 14.7998 20.7133 14.7998 20.164V19.2398C14.7998 18.6969 15.1608 18.251 15.6104 18.251H16.3704C16.82 18.251 17.181 18.6905 17.181 19.2398V20.164Z" fill="white"></path>
                        <path d="M15.9966 1.05981C9.54342 1.05981 4.2998 6.41751 4.2998 12.9967C4.2998 19.5823 9.54976 24.9335 15.9966 24.9335C22.4498 24.9335 27.6934 19.5758 27.6934 12.9967C27.6998 6.41751 22.4498 1.05981 15.9966 1.05981ZM15.9966 22.6974C10.7593 22.6974 6.49731 18.3479 6.49731 13.0031C6.49731 7.65837 10.7593 3.30888 15.9966 3.30888C21.2339 3.30888 25.4959 7.65837 25.4959 13.0031C25.5023 18.3479 21.2402 22.6974 15.9966 22.6974Z" fill="white"></path>
                        <path d="M7.06115 23.9835C6.86483 23.8284 2.19751 20.1446 2.19751 13.0031C2.19751 5.86168 6.8585 2.1714 7.06115 2.01629C7.54245 1.64791 7.64378 0.943459 7.27647 0.452283C6.9155 -0.0453554 6.22521 -0.142298 5.73758 0.226084C5.50327 0.40058 0 4.70483 0 13.0031C0 21.3014 5.50327 25.5992 5.73758 25.7801C5.9339 25.9288 6.16188 26.0063 6.3962 26.0063C6.73184 26.0063 7.06115 25.8512 7.27647 25.5604C7.63744 25.0628 7.54245 24.3583 7.06115 23.9835Z" fill="white"></path>
                        <path d="M26.2561 0.226084C25.7684 -0.142298 25.0845 -0.0453554 24.7235 0.452283C24.3626 0.943459 24.4575 1.64791 24.9388 2.02275C25.1352 2.17786 29.8025 5.86168 29.8025 13.0096C29.8025 20.151 25.1415 23.8413 24.9388 23.9964C24.4575 24.3713 24.3562 25.0692 24.7235 25.5604C24.9388 25.8577 25.2682 26.0128 25.6038 26.0128C25.8318 26.0128 26.0661 25.9417 26.2624 25.7866C26.4967 25.6057 32 21.3079 32 13.0096C32 4.69837 26.4904 0.40058 26.2561 0.226084Z" fill="white"></path>
                    </svg>
                    <svg data-v-67f0a894="" width="2.5vh" height="2.5vh" viewBox="0 0 27 27" fill="none" xmlns="http://www.w3.org/2000/svg" class={ 'carhud-container-fourth__additional__icons__seatBelt ' + (belt ? 'active' : '') }>
                        <path d="M6.08226 23.5098L3.48981 20.9174L0.901855 23.5053L3.48531 26.0982L6.08226 23.5098ZM15.2416 8.33404C15.2415 8.45338 15.1941 8.56782 15.1097 8.65219L12.5263 11.2352C12.4395 11.3222 12.3908 11.44 12.3908 11.5628C12.3908 11.6856 12.4395 11.8034 12.5263 11.8904L15.1097 14.4738C15.1967 14.5606 15.3145 14.6093 15.4373 14.6093C15.5601 14.6093 15.6779 14.5606 15.7649 14.4738L18.3479 11.8908C18.4323 11.8064 18.5467 11.759 18.6661 11.759H20.1632L15.2416 6.83689V8.33404ZM16.4003 9.95044L17.0492 10.5993C17.2184 10.7693 17.3133 10.9994 17.3133 11.2392C17.3133 11.4791 17.2184 11.7091 17.0492 11.8791L16.0768 12.8475C15.9068 13.0167 15.6768 13.1116 15.4371 13.1116C15.1973 13.1116 14.9673 13.0167 14.7974 12.8475L14.153 12.2031C13.9837 12.0332 13.8887 11.8031 13.8887 11.5632C13.8887 11.3234 13.9837 11.0933 14.153 10.9233L15.121 9.95134C15.2909 9.78241 15.5208 9.68752 15.7604 9.68735C16.0001 9.68718 16.2301 9.78175 16.4003 9.95044ZM23.5103 9.93964L23.1823 10.2677C23.0122 10.437 22.7821 10.532 22.5421 10.532C22.3022 10.532 22.072 10.437 21.902 10.2677L16.7324 5.09854C16.5631 4.92852 16.4681 4.69835 16.4681 4.45841C16.4681 4.21848 16.5631 3.98831 16.7324 3.81829L17.0609 3.48979L16.7342 3.16264C16.5615 2.9904 16.3275 2.89368 16.0835 2.89368C15.8396 2.89368 15.6056 2.9904 15.4328 3.16264L14.7866 3.80884C14.7011 3.89418 14.6333 3.99554 14.5871 4.10712C14.5408 4.2187 14.517 4.3383 14.517 4.45909C14.517 4.57988 14.5408 4.69948 14.5871 4.81106C14.6333 4.92263 14.7011 5.02399 14.7866 5.10934L21.8908 12.2135C22.0658 12.3809 22.2988 12.4743 22.541 12.4743C22.7832 12.4743 23.0162 12.3809 23.1913 12.2135L23.8375 11.5677C24.0097 11.395 24.1064 11.161 24.1064 10.917C24.1064 10.6731 24.0097 10.4391 23.8375 10.2663L23.5103 9.93964Z" fill="white"></path>
                        <path d="M26.0985 6.07101L20.9293 0.901855L17.3689 4.45416L22.5381 9.63141L26.0985 6.07866V6.07101ZM7.70158 23.8568C7.82695 23.9822 7.97578 24.0817 8.13958 24.1495C8.30339 24.2174 8.47896 24.2523 8.65626 24.2523C8.83356 24.2523 9.00913 24.2174 9.17293 24.1495C9.33674 24.0817 9.48557 23.9822 9.61093 23.8568L14.8053 18.662C14.8896 18.5776 14.937 18.4632 14.937 18.3439C14.937 18.2245 14.8896 18.1101 14.8053 18.0257L14.4687 17.6887L13.818 18.3389C13.6915 18.4656 13.5412 18.5661 13.3758 18.6347C13.2104 18.7033 13.0331 18.7386 12.8541 18.7386C12.675 18.7386 12.4977 18.7033 12.3323 18.6347C12.167 18.5661 12.0167 18.4656 11.8902 18.3389L8.66143 15.1102C8.53477 14.9836 8.43429 14.8333 8.36574 14.6678C8.29719 14.5024 8.2619 14.3251 8.2619 14.146C8.2619 13.967 8.29719 13.7896 8.36574 13.6242C8.43429 13.4588 8.53477 13.3085 8.66143 13.1819L9.31168 12.5317L8.97463 12.1946C8.89025 12.1102 8.77581 12.0629 8.65648 12.0629C8.53716 12.0629 8.42272 12.1102 8.33833 12.1946L3.14353 17.3894C3.01815 17.5148 2.91869 17.6636 2.85083 17.8274C2.78297 17.9912 2.74805 18.1668 2.74805 18.3441C2.74805 18.5214 2.78297 18.697 2.85083 18.8608C2.91869 19.0246 3.01815 19.1734 3.14353 19.2988L7.70158 23.8568Z" fill="white"></path>
                        <path d="M9.16162 14.1463C9.1614 14.2071 9.17321 14.2673 9.19638 14.3236C9.21955 14.3798 9.25362 14.4309 9.29662 14.4739L12.5254 17.7026C12.6135 17.7869 12.7308 17.8339 12.8528 17.8339C12.9747 17.8339 13.092 17.7869 13.1801 17.7026L13.8313 17.0524L9.94732 13.168L9.29662 13.8187C9.25362 13.8617 9.21955 13.9127 9.19638 13.969C9.17321 14.0252 9.1614 14.0855 9.16162 14.1463Z" fill="white"></path>
                    </svg>
                    <svg data-v-67f0a894="" width="2.5vh" height="2.5vh" viewBox="0 0 27 27" fill="none" xmlns="http://www.w3.org/2000/svg" class={ 'carhud-container-fourth__additional__icons__lock ' + (doors ? 'active' : '') }>
                        <path d="M15.6215 11.8426L27 22.9962L26.6215 24.7863L14.5201 12.9241L15.6215 11.8426Z" fill="white"></path>
                        <path d="M14.1399 13.2975L26.4691 25.383L26.4133 26.1856L22.366 26.2059L21.567 25.4227L21.9476 24.602L20.8831 24.7523L20.4647 24.3422L20.7296 21.9932L18.3732 21.9196L18.2595 20.9868L17.4993 21.0629L16.9281 20.503L16.8524 18.8625L15.6354 18.4157L15.7098 17.0737L14.8731 16.2535L12.6689 16.3291L11.8601 15.5362L14.1399 13.2975Z" fill="white"></path>
                        <path d="M0.203758 7.96469C0.0351315 7.67532 -0.0315838 7.33949 0.0139313 7.00917C0.0594444 6.67885 0.214651 6.37245 0.455536 6.1374L6.25333 0.452493C6.49232 0.214292 6.80486 0.0602615 7.14231 0.0143632C7.47976 -0.0315352 7.82322 0.0332719 8.11925 0.198703C11.5097 2.15296 14.3334 4.9268 16.321 8.2557C16.4938 8.54545 16.5632 8.88345 16.5182 9.21623C16.4732 9.54902 16.3165 9.85759 16.0727 10.0931L10.2852 15.7713C10.0445 16.0094 9.73008 16.1625 9.39119 16.2066C9.0523 16.2507 8.70807 16.1833 8.41241 16.0149C5.02102 14.0625 2.19519 11.2912 0.203758 7.96469ZM5.32555 7.6263L7.70536 5.29143C7.80818 5.19056 7.88974 5.0708 7.94538 4.939C8.00102 4.80721 8.02966 4.66595 8.02966 4.52329C8.02966 4.38063 8.00102 4.23938 7.94538 4.10758C7.88974 3.97578 7.80818 3.85603 7.70536 3.75515C7.60255 3.65428 7.48049 3.57426 7.34616 3.51967C7.21182 3.46508 7.06784 3.43698 6.92244 3.43698C6.77704 3.43698 6.63306 3.46508 6.49872 3.51967C6.36439 3.57426 6.24233 3.65428 6.13951 3.75515L3.76659 6.09002C3.66377 6.1909 3.58222 6.31065 3.52657 6.44245C3.47093 6.57425 3.44229 6.71551 3.44229 6.85816C3.44229 7.00082 3.47093 7.14208 3.52657 7.27388C3.58222 7.40567 3.66377 7.52543 3.76659 7.6263C3.86941 7.72718 3.99146 7.80719 4.1258 7.86178C4.26013 7.91638 4.40411 7.94448 4.54952 7.94448C4.69492 7.94448 4.8389 7.91638 4.97323 7.86178C5.10757 7.80719 5.22963 7.72718 5.33244 7.6263H5.32555Z" fill="white"></path>
                    </svg>
                </div>
                <div class="carhud-container-fourth__additional__splitter"></div>
                <div class="carhud-container-fourth__additional__fuel">
                    <svg data-v-67f0a894="" width="1.574074074074074vh" height="1.8518518518518516vh" viewBox="0 0 17 20" fill="none" xmlns="http://www.w3.org/2000/svg" class="carhud-container-fourth__additional__fuel__icon">
                        <path d="M15.8899 3.3507C14.2465 1.0584 10.6693 -0.592268 11.5558 0.29065C13.8162 2.54456 15.1952 4.69975 15.2989 5.18234C15.4078 5.71977 14.8582 6.5972 15.4596 6.87688C15.4751 6.88237 15.4855 6.88785 15.5011 6.89334C15.5114 8.33013 15.5011 12.8928 15.4907 16.0625C15.4855 16.1996 15.3818 17.6529 14.2776 17.6529C13.1889 17.6529 13.0748 16.1996 13.0644 16.0625V10.9021C13.0644 9.86564 12.7897 9.07047 12.2453 8.53304C11.8824 8.17658 11.4677 8.00658 11.1359 7.92432V7.32657V1.11873C11.1359 0.49904 10.6641 0 10.0783 0H1.62787C1.04723 0 0.570273 0.49904 0.570273 1.11873V18.8648H0V20H1.62787H10.0835H11.7113V18.8648H11.1411V18.4261V8.91692C11.3018 8.97724 11.478 9.07595 11.6388 9.2295C11.9913 9.58048 12.1727 10.1453 12.1727 10.9021V16.0735V16.0954C12.209 16.9619 12.6964 18.6016 14.2828 18.6016C15.8692 18.6016 16.3565 16.9619 16.3928 16.0954V16.0735C16.3928 16.0735 16.398 13.666 16.4031 11.2531C16.4031 10.0466 16.4031 8.84014 16.4031 7.92981C16.4031 7.39786 16.4031 7.03044 16.398 6.77817C16.5379 6.67398 16.652 6.54785 16.7246 6.44365C16.9216 6.16397 17.5333 5.64848 15.8899 3.3507ZM10.0731 8.23142H9.29027H2.42107H1.63824V1.71648H2.42107V1.70551H9.29027V1.711H10.0731V8.23142Z" fill="#FFB53E"></path>
                    </svg>
                    <div class="carhud-container-fourth__additional__fuel__text">
                        <span>{fuel}</span>/100л
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
{/if}