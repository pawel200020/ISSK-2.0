(function(){
    const portaled = new WeakMap();
    function portalResults(el){
        if(!el || el.dataset.portalized) return;
        const wrapper = el.closest('.autocomplete');
        if(!wrapper) return;
        el.dataset.portalized = 'true';
        function update(){
            const rect = wrapper.getBoundingClientRect();
            el.style.position = 'fixed';
            el.style.left = rect.left + 'px';
            el.style.top = rect.bottom + 'px';
            el.style.width = rect.width + 'px';
            el.style.zIndex = '2000';
        }
        // move to body
        document.body.appendChild(el);
        update();
        const obs = new MutationObserver(()=>{
            // if wrapper removed from DOM, cleanup
            if(!document.body.contains(wrapper)){
                try{ if(el.parentNode) el.parentNode.removeChild(el); }catch(e){}
                obs.disconnect();
            }
        });
        obs.observe(document.body, {childList:true, subtree:true});
        window.addEventListener('resize', update);
        window.addEventListener('scroll', update, true);
        portaled.set(el, {update, obs});
    }

    const mo = new MutationObserver(function(mutations){
        for(const m of mutations){
            for(const node of m.addedNodes){
                if(node.nodeType===1){
                    if(node.classList && node.classList.contains('autocomplete__results')) portalResults(node);
                    const found = node.querySelectorAll && node.querySelectorAll('.autocomplete__results');
                    if(found && found.length) found.forEach(portalResults);
                }
            }
        }
    });

    try{
        mo.observe(document.body, {childList: true, subtree:true});
    }catch(e){ /* document.body may not be ready - ignore */ }

    // portal any existing ones
    if(document.readyState === 'loading'){
        document.addEventListener('DOMContentLoaded', function(){ document.querySelectorAll('.autocomplete__results').forEach(portalResults); });
    } else {
        document.querySelectorAll('.autocomplete__results').forEach(portalResults);
    }
})();
