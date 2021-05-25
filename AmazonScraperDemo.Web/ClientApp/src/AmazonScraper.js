import React, { useState, useEffect } from 'react';
import axios from 'axios';

const AmazonScaper = () => {
    const [results, setResults] = useState([]);
    const [query, setQuery] = useState('');

    const searchAmazon = async () => {
        const { data } = await axios.get(`/api/scraper/scrape?query=${query}`);
        setResults(data);
    }

    return (
        <div className="container" style={{ marginTop: 60 }}>
            <div className="row">
                <div className="col-md-10">
                    <input type="text" className="form-control" onChange={e => setQuery(e.target.value)} value={query} />
                </div>
                <div className="col-md-2">
                    <button className="btn btn-primary btn-block" onClick={searchAmazon}>Search</button>
                </div>
            </div>

            <div className="row">
                <div className="col-md-12">
                    {!!results.length && <table className="table table-header table-bordered table-striped">
                        <thead>
                            <tr>
                                <th style={{ width: "20%" }}>Image</th>
                                <th>Title</th>
                                <th>Price</th>
                            </tr>
                        </thead>
                        <tbody>
                            {results.map((result, idx) => <tr key={idx}>
                                <td>
                                    <img src={result.imageUrl} />
                                </td>
                                <td>
                                    <a target="_blank" href={`https://amazon.com${result.linkUrl}`}>
                                        {result.title}
                                    </a>
                                </td>
                                <td>
                                    {result.price}
                                </td>
                            </tr>)}
                        </tbody>
                    </table>}
                </div>
            </div>
        </div>
    )
}

export default AmazonScaper;