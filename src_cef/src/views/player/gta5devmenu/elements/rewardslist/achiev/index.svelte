<script>
  import { onMount, tick } from 'svelte';
  import Award from './Award.svelte';
  import SVGComponent from './SVGComponent.svelte';
  import './achiv.css';
  import './achiv2.css';

  // incoming data from parent or websocket
  export let data = {};

  // example data for demo (keep this block)
  const dataDemo = {
    questsList: [
      { id: 'allTypes', title: 'Весь список', difficulty: 1 },
      { id: 'general',  title: 'Общие',     difficulty: 2 },
      { id: 'fun',      title: 'Развлечения',difficulty: 1 },
      { id: 'fin',      title: 'Финансы',   difficulty: 2 },
      { id: 'org',      title: 'Организация',difficulty: 3 },
      { id: 'fish',      title: 'Рыбалка',difficulty: 4 }
    ],
    goalsList: [
      {
      id: 'a1',
      byQuest: 'general',
      title: 'Бессонница',
      additionalTitle: 'Получите PayDay с 01:00 по 04:00',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 20,
      award: {
        type: 'Предмет инвентаря',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
        title: 'BIOLINK'
      }
    },
    {
      id: 'a2',
      byQuest: 'general',
      title: 'Жаворонок',
      additionalTitle: 'Получите PayDay с 05:00 по 08:00',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 20,
      award: {
        type: 'Предмет инвентаря',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
        title: 'BIOLINK'
      }
    },
    {
      id: 'a3',
      byQuest: 'fun',
      title: 'Поцелуйте игроков',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 25,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/726.png',
        title: 'Экспериментальная пилюля "Имморталитикс"'
      }
    },
    {
      id: 'a4',
      byQuest: 'general',
      title: 'Поздоровайтесь с игроками',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 50,
      award: {
        type: 'Предмет инвентаря',
        color: 'gray',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/334.png',
        title: 'ИРП Армии США'
      }
    },
    {
      id: 'a5',
      byQuest: 'general',
      title: 'Сделайте упражнения на тренажерах',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/732.png',
        title: 'Протеиновый батончик'
      }
    },
    {
      id: 'a6',
      byQuest: 'fin',
      title: 'Заработайте на угоне автомобилей',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 500000,
      award: {
        type: 'Кейс',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
        title: 'Кейс: Премиум автомобили'
      }
    },
    {
      id: 'a7',
      byQuest: 'org',
      title: 'Сыграйте в мини-игру взлома хранилища банка',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 1,
      award: {
        type: 'Предмет инвентаря',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/443.png',
        title: 'Дрель 1500w'
      }
    },
    {
      id: 'a8',
      byQuest: 'org',
      title: 'Развезите закладки',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 500,
      award: {
        type: 'Кейс',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/default.png',
        title: 'Кейс: Обычный кейс'
      }
    },
    {
      id: 'a9',
      byQuest: 'org',
      title: 'Получите купюры с ограбления бизнеса',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/333.png',
        title: 'Ручной пулемет Мk2'
      }
    },
    {
      id: 'a10',
      byQuest: 'org',
      title: 'Активируйте AirDrop',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 3,
      award: {
        type: 'Предмет инвентаря',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/337.png',
        title: 'Винтовка Marksman Mk2'
      }
    },
    {
      id: 'a11',
      byQuest: 'org',
      title: 'Верните угнанный автомобиль владельцу',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Кейс',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
        title: 'Кейс: Премиум автомобили'
      }
    },
    {
      id: 'a12',
      byQuest: 'org',
      title: 'Арестуйте преступников',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 25,
      award: {
        type: 'Донат',
        color: 'gray',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/icons/global/mc.svg',
        title: '100 MC'
      }
    },
    {
      id: 'a13',
      byQuest: 'org',
      title: 'Обменяйте грязные купюры на деньги',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 500000,
      award: {
        type: 'Кейс',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/default.png',
        title: 'Кейс: Обычный кейс'
      }
    },
    {
      id: 'a24',
      byQuest: 'fun',
      title: 'Наберите поинты в зоне дрифта',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100000,
      award: {
        type: 'Донат',
        color: 'gray',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/icons/global/mc.svg',
        title: '100 MC'
      }
    },
    {
      id: 'a14',
      byQuest: 'fin',
      title: 'Приобретите автомобиль за Majestic Coins',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 1,
      award: {
        type: 'Денежное вознаграждение',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/donate/products/100k.png',
        title: '$150 000'
      }
    },
    
    {
      id: 'a16',
      byQuest: 'general',
      title: 'Выполните мировые квесты',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 10,
      award: {
        type: 'Донат',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/icons/global/mc.svg',
        title: '300 MC'
      }
    },
    {
      id: 'a17',
      byQuest: 'fin',
      title: 'Заработайте на продаже предметов на рынке',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 500000,
      award: {
        type: 'Скидка на одежду',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/seasonPass/clothesDiscount/15.png',
        title: '15% скидка на покупку одежды'
      }
    },
    {
      id: 'a18',
      byQuest: 'org',
      title: 'Выполните семейные контракты',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/625.png',
        title: 'Адреналин'
      }
    },
    {
      id: 'a19',
      byQuest: 'general',
      title: 'Получите пособие по безработице/зарплату в PayDay',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 500,
      award: {
        type: 'Кейс',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/default.png',
        title: 'Кейс: Обычный кейс'
      }
    },
    {
      id: 'a20',
      byQuest: 'fun',
      title: 'Поднимите трубку счастливого телефона',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 15,
      award: {
        type: 'Донат',
        color: 'blue',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/icons/global/mc.svg',
        title: '200 MC'
      }
    },
    {
      id: 'a21',
      byQuest: 'general',
      title: 'Добудьте золотую шишку работая дровосеком',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 1,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/icons/global/mc.svg',
        title: '300 MC'
      }
    },
    {
      id: 'a22',
      byQuest: 'general',
      title: 'Почините детали автомобиля в СТО',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/727.png',
        title: 'Большой ремонтный набор'
      }
    },
    {
      id: 'a23',
      byQuest: 'fun',
      title: 'Соберите 21, играя в Black Jack',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'Кейс',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/default.png',
        title: 'Кейс: Обычный кейс'
      }
    },
    
    
    
    {
      id: 'a27',
      byQuest: 'org',
      title: 'Завербуйте дилеров',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 100,
      award: {
        type: 'Предмет инвентаря',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/245.png',
        title: 'Короткий пистолет'
      }
    },
    {
      id: 'a28',
      byQuest: 'fish',
      title: 'Выловите рыбу в секретной локации',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 650,
      award: {
        type: 'Предмет гардероба',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/clothesImages/44fc4367.webp ',
        title: 'Болотная панама с резинкой'
      }
    },
    {
      id: 'a29',
      byQuest: 'fish',
      title: 'Поймайте трофейную рыбу на локации 5 уровня и ниже',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 375,
      award: {
        type: 'Предмет гардероба',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/clothesImages/d02ebc00.webp',
        title: 'Коричневая камуфлированная жилетка рыбака'
      }
    },
    {
      id: 'a30',
      byQuest: 'fish',
      title: 'Поймайте трофейную рыбу на локации 6 уровня и выше',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 300,
      award: {
        type: 'Транспортное средство',
        color: 'purple',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/vehicles/dinghy.png',
        title: 'Nagasaki Dinghy'
      }
    },
    {
      id: 'a31',
      byQuest: 'fish',
      title: 'Отпустите рыбу',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 1250,
      award: {
        type: 'Предмет инвентаря',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/948.png',
        title: 'Вертушка-пуля'
      }
    },
    {
      id: 'a32',
      byQuest: 'fish',
      title: 'Выловите нелегальную рыбу',
      type: 'inprogress',
      completedvalue: 0,
      targetValue: 750,
      award: {
        type: 'Предмет гардероба',
        color: 'red',
        imagePath: 'https://cdn.majestic-files.com/public/master/static/clothesImages/5688ffe5.webp',
        title: 'Бежевые сапоги Balemciaga'
      }
    }
  ]
  };

  // use demo data if none passed
  $: localData = Object.keys(data).length ? data : dataDemo;

  let selectedQuest = null;
  let questsContainer;

  $: questsList = localData.questsList;
  $: goalsList = localData.goalsList;
  $: currentGoalsList = selectedQuest === 'allTypes'
    ? goalsList
    : goalsList.filter(g => g.byQuest === selectedQuest);
  $: selectedQuestIndex = questsList.findIndex(q => q.id === selectedQuest);
  $: leftArrowVisible = selectedQuestIndex > 0;
  $: rightArrowVisible = selectedQuestIndex < questsList.length - 1;

  function selectQuest(id) {
    selectedQuest = id;
    scrollToCurrentQuest();
  }

  function prevQuest() {
    selectedQuest =
      selectedQuestIndex <= 0
        ? questsList[questsList.length - 1].id
        : questsList[selectedQuestIndex - 1].id;
    scrollToCurrentQuest();
  }

  function nextQuest() {
    selectedQuest =
      selectedQuestIndex >= questsList.length - 1
        ? questsList[0].id
        : questsList[selectedQuestIndex + 1].id;
    scrollToCurrentQuest();
  }

  function scrollToCurrentQuest() {
    tick().then(() => {
      const items = Array.from(
        questsContainer.querySelectorAll('.quests-list__item')
      );
      const current = items[selectedQuestIndex];
      if (current) {
        questsContainer.scrollTo({ left: current.offsetLeft, behavior: 'smooth' });
      }
    });
  }

  function getAward(id) {
    dispatchEvent(new CustomEvent('getAward', { detail: id }));
  }

  onMount(() => {
    if (questsList.length) {
      selectedQuest = questsList[0].id;
    }
  });
</script>

<div class="menuWrapper-main">
  <div class="achievements full-width full-height column-block align-start justify-start">
    <!-- Quests selector -->
    <div class="quests full-width row-block align-center justify-between">
      <div class="slide-btn prev full-height" on:click={prevQuest} class:disabled={!leftArrowVisible}>
       <svg xmlns="http://www.w3.org/2000/svg" width="8.591" height="15.061" viewBox="0 0 8.591 15.061">
  <path d="M4387.549-1114.65l-7,7,7,7" transform="translate(-4379.488 1115.18)" fill="none" stroke-width="1.5"/>
</svg>
      </div>
      <div class="quests-listWrapper row-block align-center justify-start full-height">
        <div bind:this={questsContainer} class="quests-list row-block align-center justify-start full-height">
          {#each questsList as quest (quest.id)}
            <div
              class="quests-list__item column-block align-start justify-between full-height {quest.id === selectedQuest ? 'active' : ''}"
              on:click={() => selectQuest(quest.id)}
            >
              <div class="title">{quest.title}</div>
              <div class="difficulty row-block align-center justify-start">
                <span class="difficulty-text">Сложность:</span>
                {#each Array(3) as _, i}
                  <svg xmlns="http://www.w3.org/2000/svg" xmlns:xlink="http://www.w3.org/1999/xlink" viewBox="0 0 13 13">
	<g id="Группа_масок_353" data-name="Группа масок 353" transform="translate(-321 -50)" clip-path="url(#clip-path)">
		<g id="star" transform="translate(320.183 49.172)">
			<path id="Контур_16276" data-name="Контур 16276" d="M13.679,6.288,10.83,9.065l.673,3.922a.457.457,0,0,1-.664.482L7.317,11.617,3.8,13.469a.457.457,0,0,1-.664-.482L3.8,9.065.955,6.288a.457.457,0,0,1,.254-.78l3.937-.572L6.907,1.368a.476.476,0,0,1,.82,0L9.488,4.936l3.937.572a.457.457,0,0,1,.254.78Z"/>
		</g>
	</g>
</svg>
                {/each}
              </div>
            </div>
          {/each}
        </div>
      </div>
      <div class="slide-btn next full-height" on:click={nextQuest} class:disabled={!rightArrowVisible}>
       <svg xmlns="http://www.w3.org/2000/svg" width="8.591" height="15.061" viewBox="0 0 8.591 15.061">
  <path d="M4387.549-1114.65l-7,7,7,7" transform="translate(-4379.488 1115.18)" fill="none" stroke-width="1.5"/>
</svg>
      </div>
    </div>

    <!-- Goals and awards -->
    <div class="goals-list row-block align-start justify-start">
      {#each currentGoalsList as goal (goal.id)}
        <div class="goals-list__item column-block justify-between">

          <div class="goalHeader row-block justify-between align-center">
            <div class="goalHeader-main row-block align-center">
              <!-- Group input+svg in one label -->
              <label class="goalHeader-main__checkbox">
                <input
                  type="checkbox"
                  checked={goal.completedvalue >= goal.targetValue}
                  on:click|preventDefault
                />
                {#if goal.completedvalue >= goal.targetValue}
                 <svg
                    viewBox="0 0 14 11"
                    fill="none"
                    xmlns="http://www.w3.org/2000/svg"
                  >
                    <path
                      d="M1 5.18513L4.8748 9L13 1"
                      stroke="white"
                      stroke-width="1.91176"
                    />
                  </svg>
                {/if}
              </label>
               <p>
                
                {goal.title}
                {#if goal.additionalTitle}
                  <span>{goal.additionalTitle}</span>
                {/if}
              </p>
            </div>
            

            {#if goal.type !== 'completed'}
              <p class="goalHeader-progress">{goal.completedvalue} / {goal.targetValue}</p>
            {/if}
          </div>

          <div class="goals-list__item__award">
            <Award award={goal.award} />
          

    
            {#if goal.type === 'received'}
              <div class="background row-block align-center justify-center full-width full-height" on:click={() => getAward(goal.id)}>
                <div class="received row-block align-center justify-center">
              <div class="text">  Получить</div>
              </div>
              </div>
            {:else if goal.type === 'completed'}
             <div class="background row-block align-center justify-center full-width full-height" on:click={() => getAward(goal.id)}>
              <div class="completed column-block align-center justify-center">
                <div class="icon row-block align-center justify-center">
                <svg  viewBox="0 0 14 11" fill="none" xmlns="">
                    <path
                      d="M1 5.18513L4.8748 9L13 1"
                      stroke="white"
                      stroke-width="1.91176"
                    />
                  </svg>
                  </div>
                <div class="text">Выполнено</div>
              </div>
              </div>
            {/if}
          </div>
          </div>
 
      {/each}
    </div>
  </div>
</div>
