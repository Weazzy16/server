// src_client/player/achievements.js
const callRemote = mp.events.callRemote;
const call = mp.events.call;


mp.events.add('client.achievements.init', (json) => {
  try {
    const data = JSON.parse(json);
    // data должен иметь { questsList: [...], goalsList: [...] }
    achievements.set(data);
  } catch (e) {
    console.error('Achievements init parse error', e);
  }
});

// Когда от сервера прилетает апдейт одной ачивки
mp.events.add('client.achievements.update', (achId, completed) => {
  achievements.update(state => {
    const g = state.goalsList.find(x => x.AutoId === achId);
    if (g) {
      g.Completed = completed;
      g.completedvalue = completed ? g.targetValue : g.completedvalue;
    }
    return state;
  });
    });