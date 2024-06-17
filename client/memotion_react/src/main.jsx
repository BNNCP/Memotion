import React from 'react'
import ReactDOM from 'react-dom/client'
import App from './App.jsx'

const DATA = [
  { id: "todo-0", name: "Eat", completed: true },
  { id: "todo-1", name: "Sleep", completed: false },
  { id: "todo-2", name: "Repeat", completed: false },
];

const BTN = [
  { label: "All", ariapressed: true, id: "btn-01" },
  { label: "Active", ariapressed: false, id: "btn-02" },
  { label: "Completed", ariapressed: false, id: "btn-03" },
]

const root = ReactDOM.createRoot(document.getElementById('root'));
root.render(
  <App tasks={DATA} buttons={BTN} />
)
