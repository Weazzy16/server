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
      { id: 'org',      title: 'Организация',difficulty: 3 }
    ],
    goalsList: [
      {
        id: 'g1',
        byQuest: 'allTypes',
        title: 'Бессонница',
        additionalTitle: 'Получите PayDay с 01:00 по 04:00',
        type: 'inprogress',
        completedvalue: 1,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g2',
        byQuest: 'fun',
        title: 'Жаворонок',
        additionalTitle: 'Получите PayDay с 05:00 по 08:00',
        type: 'inprogress',
        completedvalue: 0,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g3',
        byQuest: 'fin',
        title: 'Заработайте на угоне автомобилей',
        type: 'inprogress',
        completedvalue: 16225,
        targetValue: 50000,
        award: {
          type: 'case',
          color: 'purple',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
          title: 'Кейс: Премиум автомобили'
        }
      },
      {
        id: 'g1',
        byQuest: 'allTypes',
        title: 'Бессонница',
        additionalTitle: 'Получите PayDay с 01:00 по 04:00',
        type: 'inprogress',
        completedvalue: 1,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g2',
        byQuest: 'fun',
        title: 'Жаворонок',
        additionalTitle: 'Получите PayDay с 05:00 по 08:00',
        type: 'inprogress',
        completedvalue: 0,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g3',
        byQuest: 'fin',
        title: 'Заработайте на угоне автомобилей',
        type: 'inprogress',
        completedvalue: 16225,
        targetValue: 50000,
        award: {
          type: 'case',
          color: 'purple',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
          title: 'Кейс: Премиум автомобили'
        }
      },
      {
        id: 'g1',
        byQuest: 'allTypes',
        title: 'Бессонница',
        additionalTitle: 'Получите PayDay с 01:00 по 04:00',
        type: 'inprogress',
        completedvalue: 1,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g2',
        byQuest: 'fun',
        title: 'Жаворонок',
        additionalTitle: 'Получите PayDay с 05:00 по 08:00',
        type: 'inprogress',
        completedvalue: 0,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g3',
        byQuest: 'fin',
        title: 'Заработайте на угоне автомобилей',
        type: 'inprogress',
        completedvalue: 16225,
        targetValue: 50000,
        award: {
          type: 'case',
          color: 'purple',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
          title: 'Кейс: Премиум автомобили'
        }
      } ,
      {
        id: 'g1',
        byQuest: 'allTypes',
        title: 'Бессонница',
        additionalTitle: 'Получите PayDay с 01:00 по 04:00',
        type: 'inprogress',
        completedvalue: 1,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g2',
        byQuest: 'fun',
        title: 'Жаворонок',
        additionalTitle: 'Получите PayDay с 05:00 по 08:00',
        type: 'inprogress',
        completedvalue: 0,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g3',
        byQuest: 'fin',
        title: 'Заработайте на угоне автомобилей',
        type: 'inprogress',
        completedvalue: 16225,
        targetValue: 50000,
        award: {
          type: 'case',
          color: 'purple',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
          title: 'Кейс: Премиум автомобили'
        }
      },
      {
        id: 'g1',
        byQuest: 'allTypes',
        title: 'Бессонница',
        additionalTitle: 'Получите PayDay с 01:00 по 04:00',
        type: 'inprogress',
        completedvalue: 1,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g2',
        byQuest: 'fun',
        title: 'Жаворонок',
        additionalTitle: 'Получите PayDay с 05:00 по 08:00',
        type: 'inprogress',
        completedvalue: 0,
        targetValue: 1,
        award: {
          type: 'item',
          color: 'blue',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/inventory/items/525.png',
          title: 'BIOLINK'
        }
      },
      {
        id: 'g3',
        byQuest: 'fin',
        title: 'Заработайте на угоне автомобилей',
        type: 'inprogress',
        completedvalue: 16225,
        targetValue: 50000,
        award: {
          type: 'case',
          color: 'purple',
          imagePath: 'https://cdn.majestic-files.com/public/master/static/img/panelMenu/donate/cases/vehicles.png',
          title: 'Кейс: Премиум автомобили'
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
