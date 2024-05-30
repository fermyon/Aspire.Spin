use spin_sdk::http::{IntoResponse, Request, Response};
use spin_sdk::{http_component, llm};

/// A simple Spin HTTP component.
#[http_component]
fn handle_api_four(req: Request) -> anyhow::Result<impl IntoResponse> {
    println!("Handling request to {:?}", req.header("spin-full-url"));

    let res = llm::infer(
        llm::InferencingModel::Llama2Chat,
        "What is the purpose of Result in Rust",
    )?;
    Ok(Response::builder()
        .status(200)
        .header("content-type", "text/plain")
        .body(res.text)
        .build())
}
