const FilterButton = (props) => {
    return (
        <button id={props.id} type="button" className="btn toggle-btn" aria-pressed={props.ariapressed}>
            <span className="visually-hidden">Show </span>
            <span>{props.label}</span>
            <span className="visually-hidden"> tasks</span>
        </button>
    )
}

export default FilterButton;