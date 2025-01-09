import { ChangeEventHandler, useEffect, useState } from "react";
import styles from "./Search.module.css";
import SearchBar from "./Searchbar";
import { TheaterShow,Show } from "./EditshowState";


interface searchData {
    firstName: string,
    lastName: string
}

const Search = () => {
    const [contentData, setContentData] = useState<Show[]>([]);
    const [searchContentData, setSearchContentData] = useState<string>('');
    const [searchDataErr, setSearchDataErr] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(``);
                const data = await response.json();
                //if fetch works but there is a data mismatch, throw specialized error
                if (!data) { throw new Error("Unable to find data") }
                else { setContentData(data) }
            } catch (error) {
                //catches auto thrown and data mismatch errors
                console.log(error);
                setSearchDataErr(true)
            }
        }
        fetchData();
    }, [])

    const searchHandler: ChangeEventHandler<HTMLInputElement> = (e) => {
        e.preventDefault();
        setSearchContentData(e.currentTarget.value);
    }

    return (
        <div className={styles.searchHolder}>
            <SearchBar
                value={searchContentData}
                placeholder={`Search by name`}
                searchHandler={searchHandler}
            />

            <div className={styles.searchData}>
                {searchDataErr && <h1 className={styles.errorMsg}>Error loading data, please check URL or console for more details!</h1>}
                <ul>
                    {
                        contentData.filter(Show =>
                            Show.Title.toLowerCase().startsWith(searchContentData.toLowerCase()) ||
                            Show.Description.toLowerCase().startsWith(searchContentData.toLowerCase()) ||
                            (`${Show.Title} ${Show.Description}`).toLowerCase().startsWith(searchContentData.toLowerCase())

                        ).map((Show, index) => <li key={index}>{Show.Title} {Show.Description}</li>)
                    }
                </ul>
            </div>
        </div>
    )
}

export default Search;