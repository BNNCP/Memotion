import React from "react";
import { useState } from "react";
import Todo from "./components/Todo";
import Form from "./components/Form";
import FilterButton from "./components/FilterButton";
import { nanoid } from "nanoid";
import "./App.css"

function App(props) {
  const [tasks, setTasks] = useState(props.tasks);
  const toggleTaskCompleted = (id) => {
    const updatedTasks = tasks.map((task) => {
      // if this task has the same ID as the edited task
      if (id === task.id) {
        // use object spread to make a new object
        // whose `completed` prop has been inverted
        return { ...task, completed: !task.completed };
      }
      return task;
    });
    setTasks(updatedTasks);
  }
  const deleteTask = (id) => {
    const remainingTasks = tasks.filter((task) => id !== task.id);
    setTasks(remainingTasks);
  }

  const taskList = tasks.map((task) => (
    <Todo id={task.id} name={task.name} completed={task.completed} key={task.id} toggleTaskCompleted={toggleTaskCompleted} deleteTask={deleteTask} />
  ));
  const addTask = (name) => {
    const newTask = { id: `todo-${nanoid()}`, name, completed: false }
    setTasks([...tasks, newTask])
  };

  const ButtonList = props.buttons.map(btn => (<FilterButton id={btn.id} ariapressed={btn.ariapressed} label={btn.label} />));

  const tasksNoun = taskList.length !== 1 ? "tasks" : "task";
  const headingText = `${taskList.length} ${tasksNoun} remaining`;


  return (
    <div className="todoapp stack-large">
      <h1>TodoMatic</h1>
      <Form onSubmit={addTask} />
      <div className="filters btn-group stack-exception">
        {ButtonList}
      </div>
      <h2 id="list-heading">
        {headingText}
      </h2>
      <ul
        role="list"
        className="todo-list stack-large stack-exception"
        aria-labelledby="list-heading"
      >
        {taskList}
      </ul>
      <button className="btn toggle-btn" type="button" onClick={() => alert("hi!")}>
        Say hi!
      </button>
    </div>
  );
}

export default App;